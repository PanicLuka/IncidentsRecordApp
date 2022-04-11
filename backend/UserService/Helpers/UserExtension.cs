using UserService.Enitites;
using UserService.Models;

namespace UserService.Helpers
{
    public static class UserExtension
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
    }
}
