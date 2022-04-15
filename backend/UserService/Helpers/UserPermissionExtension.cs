using UserService.Enitites;
using UserService.Models;

namespace UserService.Helpers
{
    public static class UserPermissionExtension
    {
        public static UserPermissionDto UserPermissionToDto(this UserPermission userPermission)
        {
            if (userPermission != null)
            {
                return new UserPermissionDto
                {
                    UserId = userPermission.UserId,
                    PermissionId = userPermission.PermissionId

                };
            }
            return null;
        }
        public static UserPermission DtoToUserPermission(this UserPermissionDto userPermissionDto)
        {
            if (userPermissionDto != null)
            {
                return new UserPermission
                {
                    UserId = userPermissionDto.UserId,
                    PermissionId = userPermissionDto.PermissionId
                };
            }
            return null;
        }
    }
}
