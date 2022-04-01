using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;
using UserService.Models;

namespace UserService.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();

            CreateMap<RoleDto, Role>()
            .ForMember(x => x.RoleId, y => y.Ignore());
            CreateMap<Role, Role>()
                .ForMember(x => x.RoleId, y => y.Ignore());
                

        }
    }
}
