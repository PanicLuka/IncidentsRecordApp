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
        public void CreateRolePermission(RolePermissionDto rolePermissionDto)
        {
            RolePermission permissionEntity = rolePermissionDto.DtoToRolePermission();

            context.Add(permissionEntity);

            SaveChanges();
        }

        public void DeleteRolePermission(RolePermissionDto rolePermissionDto)
        {
            var rolePermission = GetRolePermissionById(rolePermissionDto);

            context.Remove(rolePermission);

            SaveChanges();
        }

        public List<RolePermissionDto> GetAllRolePermissions()
        {
            var rolePermissions = context.RolePermissions.ToList();
            List<RolePermissionDto> rolePermissionDtos = new List<RolePermissionDto>();

            foreach (var permission in rolePermissions)
            {
                RolePermissionDto rolePermissionDto = permission.RolePermissionToDto();

                rolePermissionDtos.Add(rolePermissionDto);
            }

            return rolePermissionDtos;
        }

        public RolePermissionDto GetRolePermissionById(RolePermissionDto rolePermissionDto)
        {
            var permission = context.RolePermissions.FirstOrDefault
                (e => e.PermissionId == rolePermissionDto.PermissionId && e.RoleId == rolePermissionDto.RoleId);

            var permissionDto = permission.RolePermissionToDto();

            return permissionDto;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public RolePermissionDto UpdateRolePermission(Guid rolePermissionId, RolePermissionDto rolePermissionDto)
        {
            throw new NotImplementedException();
        }
    }
}
