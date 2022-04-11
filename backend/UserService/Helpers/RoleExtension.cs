

using UserService.Enitites;
using UserService.Models;

namespace UserService.Helpers
{
    public static class RoleExtension
    {
        public static RoleDto RoleToDto(this Role role)
        {
            if (role != null)
            {
                return new RoleDto
                {
                    UserType = role.UserType

                };
            }
            return null;
        }
        public static Role DtoToRole(this RoleDto roleDto)
        {
            if (roleDto != null)
            {
                return new Role
                {
                    UserType = roleDto.UserType
                };
            }
            return null;
        }
    }
}
