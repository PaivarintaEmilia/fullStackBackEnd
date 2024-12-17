using System;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class Categories
{

    public int Id { get; set; }

    public required string Name { get; set; }
    public required string Description { get; set; }

    // Connection to Users-table
    public required int UserId { get; set; }
    public required AppUser User { get; set; }

    // Cateogry can have many expenses
    public required ICollection<Expenses> Expenses { get; set; }


}
