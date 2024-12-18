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
        [Authorize(Roles = "user")] // Sisäänkirjautuneelle käyttäjälle näytetään data ja haetaan id tokenista
        [HttpGet("total-amount")]
        public async Task<ActionResult<ExpenseResTotalAmountDto>> GetAmountsSum()
        {
            // Tarvitaan kirjautuneen käyttäjän id tokenista, jotta voidaan hakea sen pohjalta oikea data

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

            if (userIdClaim == null)
            {
                return Unauthorized("No ID found from token. Sign in.");
            }

            // Muutetaan id kokonaisluvuksi
            var userId = int.Parse(userIdClaim.Value);

            // Tehdään kutsu
            var amountSum = await service.GetAmountSum(userId);
            return Ok(amountSum);
        }

        // Get Expenses ordered by Cateogries
        [HttpGet]
        public async Task<ActionResult<Dictionary<string, List<ExpenseResDto>>>> GetExpensesByCategories()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

            if (userIdClaim == null)
            {
                return Unauthorized("No ID found from token. Sign in.");
            }

            // Muutetaan id kokonaisluvuksi
            var userId = int.Parse(userIdClaim.Value);

            var listOfExpenses = await service.GetExpensesByCategories(userId);

            return Ok(listOfExpenses);

        }



        // Create an expense
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult<Expenses>> CreateExpense(ExpenseReqDto req)
        {

            var userId = await getIdTool.GetIdFromToken(HttpContext);

            if (userId == null)
            {
                return Unauthorized("No ID found from token.");
            }

            // Tarvittiin userId.Value, koska toolista tuleva id voi olla null jos sitä ei löydy
            var newExpense = await service.CreateExpense(req, userId.Value);

            return Ok(newExpense);

        }


        // Update an expense
        [HttpPatch("{id}")]
        public async Task<ExpenseResDto?> UpdateExpense(ExpenseReqDto req, int id)
        {
            
            var updatedExpense = await service.UpdateExpense(req, id);

            return updatedExpense;
        }


        // Delete Expense




    }
}
