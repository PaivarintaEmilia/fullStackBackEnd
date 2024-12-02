using System;
using API.Data.Dtos;
using API.Models;
using AutoMapper;

namespace API.Profiles;

public class UserProfile : Profile
{
    // Tämä on konstruktori
    public UserProfile()
    {
        CreateMap<AppUser, UserResDto>();
    }
}