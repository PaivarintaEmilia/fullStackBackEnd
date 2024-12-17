using System;

namespace API.Models;

public class Incomes
{
    public int Id { get; set; }
    public required decimal Amount { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedAt { get; set; }
    
    // Connection to Users-table
    public required int UserId { get; set; }
    public AppUser? User { get; set; }
}
