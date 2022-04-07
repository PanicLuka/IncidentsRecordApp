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
