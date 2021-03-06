﻿using AutoMapper;
using APP.Domain.DTOs;
using APP.Domain.Entities;
using APP.Domain.VMs;
using System.Linq;

namespace APP.API.Helpers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // MAPPINGS para DTOS
            CreateMap<User, UserForRegisterDTO>().ReverseMap();

            // MAPPINGS para VMS
            CreateMap<User, UserVM>()
                .ForMember(x => x.DeletedByUser, y => y.MapFrom(z => z.DeletedByUser.Username))
                .ReverseMap();
        }
    }
}
