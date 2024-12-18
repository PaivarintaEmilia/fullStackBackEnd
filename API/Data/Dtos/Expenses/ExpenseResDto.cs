using System;

namespace API.Data.Dtos.Expenses;

public class ExpenseResDto
{
    public int? Id { get; set; }  // Create
    public string? Description { get; set; }  // Create
    public decimal? Amount { get; set; }  // Create
    //public DateTime? CreatedAt { get; set; }
    public int? CategoryId { get; set; }  // Create
    //public int? UserId { get; set; }

}
