using System;
using System.Collections.Generic;
using UserService.Models;

namespace UserService.Service
{
    public interface IRolePermissionService
    {
        void CreateRolePermission(RolePermissionDto RolePermissionDto);
        List<RolePermissionDto> GetAllRolePermissions();
        RolePermissionDto GetRolePermissionById(RolePermissionDto RolePermissionDto);
        RolePermissionDto UpdateRolePermission(Guid RolePermissionId, RolePermissionDto RolePermissionDto);
        void DeleteRolePermission(RolePermissionDto RolePermissionDto);
        bool SaveChanges();
    }
}
