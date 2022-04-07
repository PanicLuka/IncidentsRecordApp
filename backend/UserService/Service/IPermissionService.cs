using System;
using System.Collections.Generic;
using UserService.Models;

namespace UserService.Service
{
    public interface IPermissionService
    {
        void CreatePermission(PermissionDto Permission);
        List<PermissionDto> GetAllPermissions();
        PermissionDto GetPermissionById(Guid PermissionId);
        PermissionDto UpdatePermission(Guid PermissionId, PermissionDto PermissionDto);
        void DeletePermission(Guid PermissionId);
        bool SaveChanges();

    }
}
