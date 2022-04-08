using System;
using System.Collections.Generic;
using UserService.Models;

namespace UserService.Service
{
    public interface IRoleService
    {
        void CreateRole (RoleDto role);
        List<RoleDto> GetAllRoles();
        RoleDto GetRoleById(Guid roleId);
        string GetRoleByRoleId(Guid roleId);
        RoleDto UpdateRole(Guid roleId, RoleDto role);
        void DeleteRole(Guid roleId);
        bool SaveChanges();

    }
}
