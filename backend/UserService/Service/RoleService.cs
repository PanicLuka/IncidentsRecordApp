using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using UserService.Enitites;
using UserService.Entities;
using UserService.Helpers;
using UserService.Models;
using UserService.Validators;

namespace UserService.Service
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _context;
        private readonly RoleValidator _roleValidator;

        public RoleService(DataContext context, RoleValidator roleValidator)
        {
            _context = context;
            _roleValidator = roleValidator;
        }

        public void CreateRole(RoleDto roleDto)
        {
            _roleValidator.ValidateAndThrow(roleDto);

            Role roleEntity = roleDto.DtoToRole();

            _context.Add(roleEntity);

            SaveChanges();
        }

        public void DeleteRole(Guid roleId)
        {
            var role =  GetRoleByIdHelper(roleId);

            if (role == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(role);

            SaveChanges();
        }

        public List<RoleDto> GetAllRoles()
        {
            var roles = _context.Roles.ToList();

            if(roles == null || roles.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            List<RoleDto> roleDtos = new List<RoleDto>();

            foreach (var role in roles)
            {
                RoleDto roleDto = role.RoleToDto();

                roleDtos.Add(roleDto);
            }

            return roleDtos;
        }

        public RoleDto GetRoleById(Guid roleId)
        {
            var role = _context.Roles.FirstOrDefault(e => e.RoleId == roleId);

            if (role == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var roleDto = role.RoleToDto();

            return roleDto;
        }

        public string GetRoleByRoleId(Guid roleId)
        {
            var role = GetRoleById(roleId);

            if (role == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            string roleType = role.UserType;

            return roleType;
        }

        private bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
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
                if(oldRoleDto.RoleToDto() == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                return oldRoleDto.RoleToDto();
            }

        }

        private Role GetRoleByIdHelper(Guid roleId)
        {
            var role = _context.Roles.FirstOrDefault(e => e.RoleId == roleId);

            if (role == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return role;
        }

    }
}
