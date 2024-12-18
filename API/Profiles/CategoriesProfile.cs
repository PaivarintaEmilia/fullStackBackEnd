using System;
using API.Data.Dtos.Categories;
using API.Models;
using AutoMapper;

namespace API.Profiles;

public class CategoriesProfile : Profile
{
    public CategoriesProfile()
    {
        // Model -> DTO
        CreateMap<Categories, CategoriesResDto>();
    }

}
