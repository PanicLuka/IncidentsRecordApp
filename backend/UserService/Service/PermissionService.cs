using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Enitites;
using UserService.Models;
using UserService.Helpers;
using UserService.Validators;
using FluentValidation;
using UserService.Entities;
using System.Web.Http;
using System.Net;

namespace UserService.Service
{
    public class PermissionService : IPermissionService
    {
        private readonly DataContext _context;
        private readonly PermissionValidator _validator;
        public PermissionService(DataContext context, PermissionValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public void CreatePermission(PermissionDto permissionDto)
        {
            _validator.ValidateAndThrow(permissionDto);

            Permission permissionEntity = permissionDto.DtoToPermission();

            _context.Add(permissionEntity);

            SaveChanges();
        }

        public void DeletePermission(Guid permissionId)
        {
            var permission = GetPermissionByIdHelper(permissionId);

            if (permission == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(permission);

            SaveChanges();
        }

        public PagedList<PermissionDto> GetAllPermissions(PermissionParameters permissionParameters)
        {
            var permissions = _context.Permissions.ToList();

            if(permissions == null || permissions.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            List<PermissionDto> permissionDtos = new List<PermissionDto>();

            foreach (var permission in permissions)
            {
                PermissionDto permissionDto = permission.PermissionToDto();

                permissionDtos.Add(permissionDto);
            }

            IQueryable<PermissionDto> queryable = permissionDtos.AsQueryable();

            return PagedList<PermissionDto>.ToPagedList(queryable, permissionParameters.PageNumber, permissionParameters.PageSize);
        }

        

        public PermissionDto GetPermissionById(Guid permissionId)
        {
            var permission = _context.Permissions.FirstOrDefault(e => e.PermissionId == permissionId);

            if (permission == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var permissionDto = permission.PermissionToDto();

            return permissionDto;
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

                oldPermissionDto.AccessPermission = permission.AccessPermission;

                SaveChanges();
                if(oldPermissionDto.PermissionToDto() == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                return oldPermissionDto.PermissionToDto();
            }
        }

        private bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
        private Permission GetPermissionByIdHelper(Guid permissionId)
        {
            var permission = _context.Permissions.FirstOrDefault(e => e.PermissionId == permissionId);

            if (permission == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return permission;
        }
    }
}
