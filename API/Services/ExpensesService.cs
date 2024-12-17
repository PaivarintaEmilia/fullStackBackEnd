using System;
using API.Data;
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
        // Haetaan kaikki amountit tältä kuulta ja id:n perusteella
        var amountSum = await context.Expenses
            .Where(i => i.UserId == userId && i.CreatedAt.Month == DateTime.Now.Month && i.CreatedAt.Year == DateTime.Now.Year)
            .SumAsync(i => i.Amount);

        // Palautetaan DTO 
        return new ExpenseResTotalAmountDto { TotalAmount = amountSum };
    }



    // Get Expenses with spesific dates
    // Order by categories



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


    // Delete expense by id



}

