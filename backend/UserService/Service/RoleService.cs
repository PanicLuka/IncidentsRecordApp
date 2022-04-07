using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Enitites;
using UserService.Helpers;
using UserService.Models;
using UserService.Validators;

namespace UserService.Service
{
    public class RoleService : IRoleService
    {
        private readonly DataContext context;
        private readonly RoleValidator roleValidator;

        public RoleService(DataContext context, RoleValidator roleValidator)
        {
            this.context = context;
            this.roleValidator = roleValidator;
        }

        public void CreateRole(RoleDto RoleDto)
        {
            roleValidator.ValidateAndThrow(RoleDto);

            Role roleEntity = RoleDto.DtoToRole();

            context.Add(roleEntity);

            SaveChanges();
        }

        public void DeleteRole(Guid RoleId)
        {
            var role =  GetRoleByIdHelper(RoleId);

            context.Remove(role);

            SaveChanges();
        }

        public List<RoleDto> GetAllRoles()
        {
            var roles = context.Role.ToList();
            List<RoleDto> roleDtos = new List<RoleDto>();

            foreach (var role in roles)
            {
                RoleDto roleDto = role.RoleToDto();

                roleDtos.Add(roleDto);
            }

            return roleDtos;
        }

        public RoleDto GetRoleById(Guid RoleId)
        {
            var role = context.Role.FirstOrDefault(e => e.RoleId == RoleId);

            var roleDto = role.RoleToDto();

            return roleDto;
        }

        public string GetRoleByRoleId(Guid RoleId)
        {
            var role = GetRoleById(RoleId);

            string roleType = role.UserType;

            return roleType;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public RoleDto UpdateRole(Guid roleId, RoleDto roleDto)
        {
            var oldRoleDto = GetRoleByIdHelper(roleId);

            if (oldRoleDto == null)
            {
                CreateRole(roleDto);
                return oldRoleDto.RoleToDto();
            }
            else
            {
                Role role = roleDto.DtoToRole();

                oldRoleDto.UserType = role.UserType;

                SaveChanges();
                return oldRoleDto.RoleToDto();
            }

        }

        private Role GetRoleByIdHelper(Guid RoleId)
        {
            var role = context.Role.FirstOrDefault(e => e.RoleId == RoleId);

            return role;
        }

    }
}
