using System;
using API.Data.Dtos.Expenses;
using API.Models;

namespace API.Services.Interfaces;

public interface IExpensesService
{
    // Amount-column sum from this month
    Task<ExpenseResTotalAmountDto> GetAmountSum(int userId);

    // Get expenses ordered by categoriers
    Task<Dictionary<string, List<ExpenseResDto>>> GetExpensesByCategories(int userId);

    // Create a new expense
    Task<Expenses> CreateExpense(ExpenseReqDto req, int userId);

    // Update an expense
    Task<ExpenseResDto?> UpdateExpense(ExpenseReqDto req, int id);

    // Delete an expense
    Task<string> DeleteExpenseById(int id);



}
