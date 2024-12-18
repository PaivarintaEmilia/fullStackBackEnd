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
    public async Task<List<Incomes>> GetAllIncomes(DateTimeReqDto req, int userId)
    {
        // Haetaan data päivämäärien perusteella
        var incomes = await context.Incomes
            .Where(i => i.Id == userId && i.CreatedAt >= req.StartingDate && i.CreatedAt <= req.EndingDate)
            .ToListAsync(); // Incomes-mallit listana

        return incomes;
    }

    // Create Income
    public async Task<Incomes> CreateIncome(IncomeDto req, int userId)
    {
        // Luodaan income
        var newIncome = new Incomes
        {
            Description = req.Description,
            Amount = req.Amount,
            CreatedAt = DateTime.Now,
            UserId = userId
        };

        // Tietokantaan lisäys
        context.Incomes.Add(newIncome);
        // Commit
        await context.SaveChangesAsync();


        // return
        return newIncome;
    }

    // Edit income 
    public async Task<IncomeDto?> UpdateIncome(IncomeDto req, int id)
    {

        var income = await context.Incomes.FirstOrDefaultAsync(income => income.Id == id);

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

    // Delete Income by id
    public async Task<string> DeleteIncomeById(int id)
    {
        // Search the income by id
        var income = await context.Incomes.FirstOrDefaultAsync(income => income.Id == id);

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


}
