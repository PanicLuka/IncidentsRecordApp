using System;
using System.Collections.Generic;
using UserService.Models;

namespace UserService.Service
{
    public interface IUserPermissionsService
    {
        void CreateUserPermission(UserPermissionDto userPermissionDto);
        List<UserPermissionDto> GetAllUserPermissions();
        UserPermissionDto GetUserPermissionById(UserPermissionDto userPermissionDto);
        UserPermissionDto UpdateUserPermission(Guid userPermissionId, UserPermissionDto userPermissionDto);
        void DeleteUserPermission(UserPermissionDto userPermissionDto);
        bool SaveChanges();
    }
}
