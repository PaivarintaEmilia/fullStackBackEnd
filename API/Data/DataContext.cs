using System;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    // Propertyn nimestä tulee
    // tietokantataulun nimi
    // Tässä siis Users

    // modeleista tehdään dbsetit
    public required DbSet<AppUser> Users { get; set; }
    public required DbSet<Categories> Categories { get; set; }
    public required DbSet<Expenses> Expenses { get; set; }
    public required DbSet<Incomes> Incomes { get; set; }


    // Configuration of the tables
    protected override void OnModelCreating(ModelBuilder builder)
    {
            // Users-taulu: Ei muutoksia tässä
            builder.Entity<AppUser>(entity =>
            {
                entity.HasKey(u => u.Id); // Asetetaan pääavain
            });

            // Income-taulu: Linkitetään Income-taulu Users-tauluun
            builder.Entity<Incomes>(entity =>
            {
                entity.HasKey(i => i.Id);
                // Connection to Users-table
                entity.HasOne(i => i.User) // Income has one user
                    .WithMany(u => u.Incomes) // User can have many Incomes
                    .HasForeignKey(i => i.UserId) // Connecting key in Income
                    .OnDelete(DeleteBehavior.Cascade); // Users incomes are deleted during users deletion
            });

            // Categories-table's connections
            builder.Entity<Categories>(entity => {
                entity.HasKey(c => c.Id);
                // Connection to Users-table
                entity.HasOne(c => c.User) // One category has one user
                    .WithMany(u => u.Categories) // One user can have many categories
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // When user is deleted then users categories are deleted
            });

            // Expenses-table's connections
            builder.Entity<Expenses>(entity =>
            {
                entity.HasKey(e => e.Id);
                // Connection to Users-table
                entity.HasOne(e => e.User) // Expense has one user
                    .WithMany(u => u.Expenses) // User can have many expenses 
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // Users expenses are deleted during users deletion

                entity.HasOne(e => e.Category) // Expense has one category
                    .WithMany(c => c.Expenses) // One category can have many expenses
                    .HasForeignKey(e => e.CategoryId);

            });

    }
}
