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
    public class UserPermissionService : IUserPermissionsService
    {
        private readonly DataContext _context;

        public UserPermissionService(DataContext context)
        {
            _context = context;
        }
        public void CreateUserPermission(UserPermissionDto userPermissionDto)
        {
            UserPermission permissionEntity = userPermissionDto.DtoToUserPermission();

            _context.Add(permissionEntity);

            SaveChanges();
        }

        public void DeleteUserPermission(UserPermissionDto userPermissionDto)
        {
            var userPermission = GetUserPermissionById(userPermissionDto);

            if (userPermission == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(userPermission);

            SaveChanges();
        }

        public List<UserPermissionDto> GetAllUserPermissions()
        {
            var userPermissions = _context.UserPermissions.ToList();

            if (userPermissions == null || userPermissions.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            List<UserPermissionDto> userPermissionDtos = new List<UserPermissionDto>();

            foreach (var permission in userPermissions)
            {
                UserPermissionDto userPermissionDto = permission.UserPermissionToDto();

                userPermissionDtos.Add(userPermissionDto);
            }

            return userPermissionDtos;
        }

        public UserPermissionDto GetUserPermissionById(UserPermissionDto userPermissionDto)
        {
            var permission = _context.UserPermissions.FirstOrDefault
                (e => e.PermissionId == userPermissionDto.PermissionId && e.UserId == userPermissionDto.UserId);

            if (permission == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var permissionDto = permission.UserPermissionToDto();

            return permissionDto;
        }

        private bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public UserPermissionDto UpdateUserPermission(Guid userPermissionId, UserPermissionDto userPermissionDto)
        {
            throw new NotImplementedException();
        }

        
    }
}
