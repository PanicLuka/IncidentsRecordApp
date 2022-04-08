using System;
using UserService.Enitites;
using UserService.Models;

namespace UserService.Service
{
    public interface IPermissionService
    {
        void CreatePermission(PermissionDto permission);
        PagedList<PermissionDto> GetAllPermissions(PermissionParameters permissionParameters);
        PermissionDto GetPermissionById(Guid permissionId);
        PermissionDto UpdatePermission(Guid permissionId, PermissionDto permissionDto);
        void DeletePermission(Guid permissionId);
        bool SaveChanges();

    }
}
