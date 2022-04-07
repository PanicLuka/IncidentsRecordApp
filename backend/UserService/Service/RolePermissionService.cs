using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Enitites;
using UserService.Models;
using UserService.Helpers;

namespace UserService.Service
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly DataContext context;

        public RolePermissionService(DataContext context)
        {
            this.context = context;
        }
        public void CreateRolePermission(RolePermissionDto RolePermissionDto)
        {
            RolePermission permissionEntity = RolePermissionDto.DtoToRolePermission();

            context.Add(permissionEntity);

            SaveChanges();
        }

        public void DeleteRolePermission(RolePermissionDto RolePermissionDto)
        {
            var rolePermission = GetRolePermissionById(RolePermissionDto);

            context.Remove(rolePermission);

            SaveChanges();
        }

        public List<RolePermissionDto> GetAllRolePermissions()
        {
            var rolePermissions = context.rolePermissions.ToList();
            List<RolePermissionDto> rolePermissionDtos = new List<RolePermissionDto>();

            foreach (var permission in rolePermissions)
            {
                RolePermissionDto rolePermissionDto = permission.RolePermissionToDto();

                rolePermissionDtos.Add(rolePermissionDto);
            }

            return rolePermissionDtos;
        }

        public RolePermissionDto GetRolePermissionById(RolePermissionDto RolePermissionDto)
        {
            var permission = context.rolePermissions.FirstOrDefault
                (e => e.PermissionId == RolePermissionDto.PermissionId && e.RoleId == RolePermissionDto.RoleId);

            var permissionDto = permission.RolePermissionToDto();

            return permissionDto;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public RolePermissionDto UpdateRolePermission(Guid RolePermissionId, RolePermissionDto RolePermissionDto)
        {
            throw new NotImplementedException();
        }
    }
}
