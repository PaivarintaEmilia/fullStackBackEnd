using System;

namespace API.Models;

public class Expenses
{
    public int Id { get; set; }

    public required decimal Amount { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedAt { get; set; }

    // Connection to Users-table
    public required int UserId { get; set; }
    public virtual AppUser? User { get; set; }

    // Connection to Categories-table
    public required int CategoryId { get; set; }
    public virtual Categories? Category { get; set; }



}
