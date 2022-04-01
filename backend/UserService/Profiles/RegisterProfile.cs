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
            CreateMap<User, UserDto>();
              
                
            CreateMap<UserDto, User>()
                .ForMember(x => x.UserId, y => y.Ignore());
            CreateMap<User, User>()
                .ForMember(x => x.UserId, y => y.Ignore())
                .ForMember(x => x.RoleId, y => y.Ignore());
        }
    }
}
