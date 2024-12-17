using System;
using API.Data.Dtos;
using API.Data.Dtos.Incomes;
using API.Models;

namespace API.Services.Interfaces;

public interface IIncomesService
{
    // Total amount of Amount-column from the spesific month
    Task<IncomeResTotalAmountDto> GetAmountSum(int userId);

    // Get all incomes from spesific times
    Task<List<Incomes>> GetAllIncomes(DateTimeReqDto req);

    // Edit Income
    Task<IncomeDto?> UpdateIncome(IncomeDto req);


    // Delete income
    Task<string?> DeleteIncomeById(int incomeId);

}
