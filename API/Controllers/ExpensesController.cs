using API.Data.Dtos;
using API.Data.Dtos.Expenses;
using API.Models;
using API.Services.Interfaces;
using API.Tools.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController(IExpensesService service, IGetIdTool getIdTool) : ControllerBase
    {

        // Get Amount-columns sum from this month
        [Authorize(Roles = "user,admin")]
        [HttpGet("total-amount")]
        public async Task<ActionResult<ExpenseResTotalAmountDto>> GetAmountsSum()
        {

            var userId = await getIdTool.GetIdFromToken(HttpContext);

            if (userId == null)
            {
                return Unauthorized("No ID found from token.");
            }

            // Tehdään kutsu
            var amountSum = await service.GetAmountSum(userId.Value);
            return Ok(amountSum);
            // return DTO return new ExpenseResTotalAmountDto { TotalAmount = amountSum };
        }


        // Get Expenses ordered by Cateogries with specific dates
        [Authorize(Roles = "user,admin")]
        [HttpGet]
        public async Task<ActionResult<Dictionary<string, List<ExpenseResDto>>>> GetExpensesByCategories([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var userId = await getIdTool.GetIdFromToken(HttpContext);

            if (userId == null)
            {
                return Unauthorized("No ID found from token.");
            }

            Console.WriteLine($"Received startDate: {startDate}, endDate: {endDate}");

            var req = new DateTimeReqDto
            {
                StartingDate = startDate,
                EndingDate = endDate
            };

            var listOfExpenses = await service.GetExpensesByCategories(req, userId.Value);

            return Ok(listOfExpenses);

        }


        // Create expense
        [Authorize(Roles = "user,admin")]
        [HttpPost]
        public async Task<ActionResult<ExpenseResDto>> CreateExpense(ExpenseReqDto req)
        {

            var userId = await getIdTool.GetIdFromToken(HttpContext);

            if (userId == null)
            {
                return Unauthorized("No ID found from token.");
            }

            // Tarvittiin userId.Value, koska toolista tuleva id voi olla null jos sitä ei löydy
            var newExpense = await service.CreateExpense(req, userId.Value);

            return Ok(new ExpenseResDto { Id = newExpense.Id, Description = newExpense.Description, Amount = newExpense.Amount, CategoryId = newExpense.CategoryId });

        }


        // Update expense
        [Authorize(Roles = "user,admin")]
        [HttpPatch("{id}")]
        public async Task<ExpenseResDto?> UpdateExpense(ExpenseReqDto req, int id)
        {
            var updatedExpense = await service.UpdateExpense(req, id);

            return updatedExpense;
        }


        // Delete Expense
        [Authorize(Roles = "user,admin")]
        [HttpDelete("{id}")]
        public async Task<DeleteResDto> DeleteExpenseById(int id)
        {
            var deleteResponse = await service.DeleteExpenseById(id);

            return new DeleteResDto { Message = deleteResponse };
        }




    }
}
