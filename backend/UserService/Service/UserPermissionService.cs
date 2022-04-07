using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Enitites;
using UserService.Models;
using UserService.Helpers;

namespace UserService.Service
{
    public class UserPermissionService : IUserPermissionsService
    {
        private readonly DataContext context;

        public UserPermissionService(DataContext context)
        {
            this.context = context;
        }
        public void CreateUserPermission(UserPermissionDto userPermissionDto)
        {
            UserPermission permissionEntity = userPermissionDto.DtoToUserPermission();

            context.Add(permissionEntity);

            SaveChanges();
        }

        public void DeleteUserPermission(UserPermissionDto userPermissionDto)
        {
            var userPermission = GetUserPermissionById(userPermissionDto);

            context.Remove(userPermission);

            SaveChanges();
        }

        public List<UserPermissionDto> GetAllUserPermissions()
        {
            var userPermissions = context.UserPermissions.ToList();
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
            var permission = context.UserPermissions.FirstOrDefault
                (e => e.PermissionId == userPermissionDto.PermissionId && e.UserId == userPermissionDto.UserId);

            var permissionDto = permission.UserPermissionToDto();

            return permissionDto;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public UserPermissionDto UpdateUserPermission(Guid UserPermissionId, UserPermissionDto userPermissionDto)
        {
            throw new NotImplementedException();
        }

        
    }
}
