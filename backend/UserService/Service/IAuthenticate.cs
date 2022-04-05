using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;
using UserService.Models;

namespace UserService.Service
{
    public interface IAuthenticate
    {
       string GenerateToken(UserLogin user);

       bool VerifyPassword(UserLogin user);

    }
}
