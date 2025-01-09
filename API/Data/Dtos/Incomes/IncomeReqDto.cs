using System;

namespace API.Data.Dtos.Incomes;

public class IncomeReqDto
{
    public required string Description { get; set; }
    public required decimal Amount { get; set; }

}
