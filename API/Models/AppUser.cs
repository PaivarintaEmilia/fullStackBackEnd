using System;

namespace API.Models;

public class AppUser
{

    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public required byte[] HashedPassword { get; set; }

    // Connection to Income-table
    // Yksi käyttäjä voi omistaa useita tuloja (Income)
    public ICollection<Incomes>? Incomes { get; set; }

    // User can have many expenses
    public ICollection<Expenses>? Expenses { get; set; }

    // User can have many categories
    public ICollection<Categories>? Categories { get; set; }

}