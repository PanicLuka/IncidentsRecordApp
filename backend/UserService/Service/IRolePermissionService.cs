using System;
using System.Collections.Generic;
using UserService.Models;

namespace UserService.Service
{
    public interface IRolePermissionService
    {
        void CreateRolePermission(RolePermissionDto rolePermissionDto);
        List<RolePermissionDto> GetAllRolePermissions();
        RolePermissionDto GetRolePermissionById(RolePermissionDto rolePermissionDto);
        RolePermissionDto UpdateRolePermission(Guid rolePermissionId, RolePermissionDto rolePermissionDto);
        void DeleteRolePermission(RolePermissionDto rolePermissionDto);
        bool SaveChanges();
    }
}
