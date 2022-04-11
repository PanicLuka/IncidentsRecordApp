using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Enitites;
using UserService.Models;
using UserService.Helpers;
using UserService.Entities;
using System.Web.Http;
using System.Net;

namespace UserService.Service
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly DataContext _context;

        public RolePermissionService(DataContext context)
        {
            _context = context;
        }
        public void CreateRolePermission(RolePermissionDto rolePermissionDto)
        {
            RolePermission permissionEntity = rolePermissionDto.DtoToRolePermission();

            _context.Add(permissionEntity);

            SaveChanges();
        }

        public void DeleteRolePermission(RolePermissionDto rolePermissionDto)
        {
            var rolePermission = GetRolePermissionById(rolePermissionDto);

            if(rolePermission == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(rolePermission);

            SaveChanges();
        }

        public List<RolePermissionDto> GetAllRolePermissions()
        {
            var rolePermissions = _context.RolePermissions.ToList();

            if (rolePermissions == null || rolePermissions.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }


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
            var permission = _context.RolePermissions.FirstOrDefault
                (e => e.PermissionId == rolePermissionDto.PermissionId && e.RoleId == rolePermissionDto.RoleId);

            if (permission == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var permissionDto = permission.RolePermissionToDto();

            return permissionDto;
        }

        private bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public RolePermissionDto UpdateRolePermission(Guid rolePermissionId, RolePermissionDto rolePermissionDto)
        {
            throw new NotImplementedException();
        }
    }
}
