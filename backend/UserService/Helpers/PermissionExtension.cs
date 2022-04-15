using UserService.Enitites;
using UserService.Models;

namespace UserService.Helpers
{
    public static class PermissionExtension
    {
        public static PermissionDto PermissionToDto(this Permission permission)
        {
            if (permission != null)
            {
                return new PermissionDto
                {
                    AccessPermission = permission.AccessPermission

                };
            }
            return null;
        }
        public static Permission DtoToPermission(this PermissionDto permissionDto)
        {
            if (permissionDto != null)
            {
                return new Permission
                {
                    AccessPermission = permissionDto.AccessPermission
                };
            }
            return null;
        }
    }
}
