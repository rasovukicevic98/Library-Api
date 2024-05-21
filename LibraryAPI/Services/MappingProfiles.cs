﻿using AutoMapper;
using LibraryAPI.Dto;
using LibraryAPI.Migrations;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Services
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserDto, IdentityUser>().ReverseMap();
            CreateMap<UpdateUser, IdentityUser>().ReverseMap();
        }
    }
}
