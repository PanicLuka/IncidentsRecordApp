using UserService.Enitites;
using UserService.Models;

namespace UserService.Helpers
{
    public static class Extension
    {
        public static UserDto UserToDto(this User user)
        {
            if (user != null)
            {
                return new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password
                };
            }
            return null;
        }
        public static User DtoToUser(this UserDto userDto)
        {
            if (userDto != null)
            {
                return new User
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    Password = userDto.Password
                };
            }
            return null;
        }

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

        public static PermissionDto PermissionToDto(this Permission permission)
        {
            if (permission != null)
            {
                return new PermissionDto
                {
                    AccessPermissions = permission.AccessPermissions

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
                    AccessPermissions = permissionDto.AccessPermissions
                };
            }
            return null;
        }
    }
}
