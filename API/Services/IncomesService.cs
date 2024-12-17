using System;
using API.Data;
using API.Data.Dtos;
using API.Data.Dtos.Incomes;
using API.Models;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class IncomesService(DataContext context) : IIncomesService
{


    // Get Amount-column sum from this month
    public async Task<IncomeResTotalAmountDto> GetAmountSum(int userId)
    {
        // Haetaan kaikki amountit ja lisätään ne yhteen
        var amountSum = await context.Incomes
            .Where(i => i.UserId == userId && i.CreatedAt.Month == DateTime.Now.Month && i.CreatedAt.Year == DateTime.Now.Year)
            .SumAsync(i => i.Amount);

        // Palautetaan DTO
        return new IncomeResTotalAmountDto { TotalAmount = amountSum };

    }

    // Get incomes with spesific dates
    public async Task<List<Incomes>> GetAllIncomes(DateTimeReqDto req)
    {
        // Haetaan data päivämäärien perusteella
        var incomes = await context.Incomes
            .Where(i => i.CreatedAt >= req.StartingDate && i.CreatedAt <= req.EndingDate)
            .ToListAsync(); // Incomes-mallit listana

        return incomes;
    }

    // Delete Income by id
    public async Task<string?> DeleteIncomeById(int incomeId)
    {
        // Search the income by id
        var income = await context.Incomes.FirstOrDefaultAsync(income => income.Id == incomeId);

        // Return message if income wasn't found
        if (income == null)
        {
            return "No income found with this id. Deletion unsuccessfull.";
        };

        // Delete the income
        context.Incomes.Remove(income);

        // Save changes
        await context.SaveChangesAsync();

        return "Income deleted.";
    }

    // Edit income 
    public async Task<IncomeDto?> UpdateIncome(IncomeDto req)
    {

        var income = await context.Incomes.FirstOrDefaultAsync(income => income.Id == req.Id);

        if (income != null)
        {
            income.Description = req.Description;
            income.Amount = req.Amount;
            await context.SaveChangesAsync();
            // Muunnetaan Category -> CategoryResDto
            return new IncomeDto
            {
                Id = income.Id,
                Description = income.Description,
                Amount = income.Amount
            };
        }

        return null;
    }
}
