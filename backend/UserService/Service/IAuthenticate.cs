using UserService.Models;

namespace UserService.Service
{
    public interface IAuthenticate
    {
       string GenerateToken(UserLogin user);
       bool VerifyPassword(UserLogin user);

    }
}
