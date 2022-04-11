
using UserService.Enitites;
using UserService.Models;

namespace UserService.Helpers
{
    public static class RolePermissionExtension
    {
        public static RolePermissionDto RolePermissionToDto(this RolePermission rolePermission)
        {
            if (rolePermission != null)
            {
                return new RolePermissionDto
                {
                    RoleId = rolePermission.RoleId,
                    PermissionId = rolePermission.PermissionId

                };
            }
            return null;
        }
        public static RolePermission DtoToRolePermission(this RolePermissionDto rolePermissionDto)
        {
            if (rolePermissionDto != null)
            {
                return new RolePermission
                {
                    RoleId = rolePermissionDto.RoleId,
                    PermissionId = rolePermissionDto.PermissionId
                };
            }
            return null;
        }
    }
}
