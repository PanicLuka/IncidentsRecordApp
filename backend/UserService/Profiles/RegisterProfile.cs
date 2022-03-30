using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;
using UserService.Models;

namespace UserService.Profiles
{
    public class RegisterProfile : Profile
    {
        public RegisterProfile()
        {
            CreateMap<Register, RegisterDto>();
            CreateMap<RegisterDto, Register>();
        }
    }
}
