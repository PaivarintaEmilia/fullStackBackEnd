using System;

namespace API.Data.Dtos.Expenses;

public class ExpenseReqDto
{
    public required string Description { get; set; }
    public required decimal Amount { get; set; }
    public int CategoryId { get; set; }

}
