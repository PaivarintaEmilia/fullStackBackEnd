using System;
using API.Data.Dtos.Expenses;
using API.Models;

namespace API.Services.Interfaces;

public interface IExpensesService
{
    // Amount-column sum from this month
    Task<ExpenseResTotalAmountDto> GetAmountSum(int userId);

    // Create a new expense
    Task<Expenses> CreateExpense(ExpenseReqDto req, int userId);

}
