using System;
using API.Data;
using API.Data.Dtos;
using API.Data.Dtos.Expenses;
using API.Models;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class ExpensesService(DataContext context) : IExpensesService
{


    // Get Amount-column sum from this month
    public async Task<ExpenseResTotalAmountDto> GetAmountSum(int userId)
    {
        // Get all amounts with the correct userId and the current month and year
        var amounts = await context.Expenses
            .Where(i => i.UserId == userId && i.CreatedAt.Month == DateTime.Now.Month && i.CreatedAt.Year == DateTime.Now.Year)
            .ToListAsync();

        // Sum the specific data from db
        var amountSum = amounts.Sum(i => i.Amount);

        // Return data DTO 
        return new ExpenseResTotalAmountDto { TotalAmount = amountSum };
    }



    // Get Expenses with spesific dates
    // Order by categories
    public async Task<Dictionary<string, List<ExpenseResDto>>> GetExpensesByCategories(DateTimeReqDto req, int userId)
    {
        // Haetaan käyttäjän expenset lajiteltuna cateogrioiden mukaan

        // 1. Suodatetaan päivämäärien mukaan
        var query = context.Expenses
            .Where(e => e.UserId == userId && e.CreatedAt.Date >= req.StartingDate.Date && e.CreatedAt.Date <= req.EndingDate.Date)
            .Include(e => e.Category);

        // 2. Haetaan tietokannan tiedot
        var listOfExpenses = await query.ToListAsync();

        // 3. Lajitellaan kategorioittain
        var groupedExpenses = listOfExpenses
            .Where(e => !string.IsNullOrEmpty(e.Category?.Name)) // Varmista, että kategoriassa on nimi
            .GroupBy(e => e.Category.Name)
            .ToDictionary(
                group => group.Key, // Kategorian nimi
                group => group.Select(e => new ExpenseResDto
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.Amount,
                    CategoryId = e.CategoryId
                }).ToList()
            );

        return groupedExpenses;
    }




    // Create new Expense
    public async Task<Expenses> CreateExpense(ExpenseReqDto req, int userId)
    {
        // Luodaan expense
        var newExpense = new Expenses
        {
            Description = req.Description,
            Amount = req.Amount,
            CreatedAt = DateTime.Now,
            CategoryId = req.CategoryId,
            UserId = userId
        };

        // Tietokantaan lisäys
        context.Expenses.Add(newExpense);
        // Commit
        await context.SaveChangesAsync();


        // return
        return newExpense;
    }

    // Update expense by id
    public async Task<ExpenseResDto?> UpdateExpense(ExpenseReqDto req, int id)
    {
        // Haetaan Expense
        var expense = await context.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);

        // Päivitetään expense tietyllä id:llä
        if (expense != null)
        {
            expense.Description = req.Description;
            expense.Amount = req.Amount;
            expense.CategoryId = req.CategoryId;
            // Tallennetaan muutetut tiedot
            await context.SaveChangesAsync();

            // Muutetaan Expense -> ExpenseResDto
            return new ExpenseResDto
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                CategoryId = expense.CategoryId
            };
        }

        return null;
    }

    // Delete expense by id
    public async Task<string> DeleteExpenseById(int id)
    {
        var expense = await context.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);

        if (expense == null)
        {
            return "No expense found with the specific id. Deletion unsuccessfull.";
        }

        context.Expenses.Remove(expense);

        await context.SaveChangesAsync();

        return "Expense deleted.";
    }
}

