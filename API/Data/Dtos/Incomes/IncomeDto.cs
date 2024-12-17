using System;

namespace API.Data.Dtos.Incomes;

public class IncomeDto
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public required decimal Amount { get; set; }

}
