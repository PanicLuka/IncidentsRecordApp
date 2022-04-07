using System;
using System.Collections.Generic;
using UserService.Models;

namespace UserService.Service
{
    public interface IRoleService
    {
        void CreateRole (RoleDto Role);
        List<RoleDto> GetAllRoles();
        RoleDto GetRoleById(Guid RoleId);
        string GetRoleByRoleId(Guid RoleId);
        RoleDto UpdateRole(Guid roleId, RoleDto Role);
        void DeleteRole(Guid RoleId);
        bool SaveChanges();

    }
}
