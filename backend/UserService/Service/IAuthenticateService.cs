using UserService.Models;

namespace UserService.Service
{
    public interface IAuthenticateService
    {
       string GenerateToken(UserLogin user);
       bool VerifiedPassword(UserLogin user);

    }
}
