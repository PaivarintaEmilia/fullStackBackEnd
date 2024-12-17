using System;
using API.Data.Dtos.Incomes;
using API.Models;
using AutoMapper;

namespace API.Profiles;

public class IncomesProfile : Profile
{
    public IncomesProfile()
    {
        // Model --> DTO
        CreateMap<Incomes, IncomeDto>();

    }

}
