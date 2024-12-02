using System;
using API.Data.Dtos;
using API.Models;
using AutoMapper;

namespace API.Profiles;

public class ProductProfile : Profile
{

    public ProductProfile()
    {
        // AppProduct -> ProductResDto
        CreateMap<Product, ProductResDto>();


    }


}
