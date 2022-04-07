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
        public void CreatePermission(PermissionDto PermissionDto)
        {
            validator.ValidateAndThrow(PermissionDto);

            Permission permissionEntity = PermissionDto.DtoToPermission();

            context.Add(permissionEntity);

            SaveChanges();
        }

        public void DeletePermission(Guid PermissionId)
        {
            var permission = GetPermissionByIdHelper(PermissionId);

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

        public PermissionDto GetPermissionById(Guid PermissionId)
        {
            var permission = context.Permission.FirstOrDefault(e => e.PermissionId == PermissionId);

            var permissionDto = permission.PermissionToDto();

            return permissionDto;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public PermissionDto UpdatePermission(Guid PermissionId, PermissionDto PermissionDto)
        {
            var oldPermissionDto = GetPermissionByIdHelper(PermissionId);

            if (oldPermissionDto == null)
            {
                CreatePermission(PermissionDto);
                return oldPermissionDto.PermissionToDto();
            }
            else
            {
                Permission permission = PermissionDto.DtoToPermission();

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
