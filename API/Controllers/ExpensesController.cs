using API.Data.Dtos.Expenses;
using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController(IExpensesService service) : ControllerBase
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

        // Create an expense
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult<Expenses>> CreateExpense(ExpenseReqDto req)
        {
            // Katso miten parhaiten voi muokata tämän niin ettei tarvita toistoa
            // Tarvitaan kirjautuneen käyttäjän id tokenista, jotta voidaan hakea sen pohjalta oikea data

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

            if (userIdClaim == null)
            {
                return Unauthorized("No ID found from token. Sign in.");
            }

            // Muutetaan id kokonaisluvuksi
            var userId = int.Parse(userIdClaim.Value);

            var newExpense = await service.CreateExpense(req, userId);

            return Ok(newExpense);
            
        }

       
    }
}
