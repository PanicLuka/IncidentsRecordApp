using System;
using System.Collections.Generic;
using UserService.Models;

namespace UserService.Service
{
    public interface IPermissionService
    {
        void CreatePermission(PermissionDto permission);
        List<PermissionDto> GetAllPermissions();
        PermissionDto GetPermissionById(Guid permissionId);
        PermissionDto UpdatePermission(Guid permissionId, PermissionDto permissionDto);
        void DeletePermission(Guid permissionId);
        bool SaveChanges();

    }
}
