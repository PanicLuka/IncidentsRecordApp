using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Enitites;
using UserService.Models;
using UserService.Helpers;
using UserService.Validators;
using FluentValidation;

namespace UserService.Service
{
    public class PermissionService : IPermissionService
    {
        private readonly DataContext context;
        private readonly PermissionValidator validator;
        public PermissionService(DataContext context, PermissionValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }
        public void CreatePermission(PermissionDto permissionDto)
        {
            validator.ValidateAndThrow(permissionDto);

            Permission permissionEntity = permissionDto.DtoToPermission();

            context.Add(permissionEntity);

            SaveChanges();
        }

        public void DeletePermission(Guid permissionId)
        {
            var permission = GetPermissionByIdHelper(permissionId);

            context.Remove(permission);

            SaveChanges();
        }

        public List<PermissionDto> GetAllPermissions()
        {
            var permissions = context.Permission.ToList();
            List<PermissionDto> permissionDtos = new List<PermissionDto>();

            foreach (var permission in permissions)
            {
                PermissionDto permissionDto = permission.PermissionToDto();

                permissionDtos.Add(permissionDto);
            }

            return permissionDtos;
        }

        public PermissionDto GetPermissionById(Guid permissionId)
        {
            var permission = context.Permission.FirstOrDefault(e => e.PermissionId == permissionId);

            var permissionDto = permission.PermissionToDto();

            return permissionDto;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public PermissionDto UpdatePermission(Guid permissionId, PermissionDto permissionDto)
        {
            var oldPermissionDto = GetPermissionByIdHelper(permissionId);

            if (oldPermissionDto == null)
            {
                CreatePermission(permissionDto);
                return oldPermissionDto.PermissionToDto();
            }
            else
            {
                Permission permission = permissionDto.DtoToPermission();

                oldPermissionDto.AccessPermissions = permission.AccessPermissions;

                SaveChanges();
                return oldPermissionDto.PermissionToDto();
            }
        }

        private Permission GetPermissionByIdHelper(Guid permissionId)
        {
            var permission = context.Permission.FirstOrDefault(e => e.PermissionId == permissionId);

            return permission;
        }
    }
}
