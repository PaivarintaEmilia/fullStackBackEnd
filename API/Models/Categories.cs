using System;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class Categories
{

    public int Id { get; set; }

    public required string Name { get; set; }

    public bool UserDefined { get; set; } = true;

    // Connection to Users-table
    public required int UserId { get; set; }
    public virtual AppUser? User { get; set; }

    // Cateogry can have many expenses
    public virtual ICollection<Expenses>? Expenses { get; set; }


}
