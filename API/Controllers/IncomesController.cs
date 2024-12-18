using API.Data.Dtos;
using API.Data.Dtos.Incomes;
using API.Models;
using API.Services.Interfaces;
using API.Tools.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController(IIncomesService service, IMapper mapper, IGetIdTool getIdTool) : ControllerBase
    {
        // Get Amount-columns sum
        [Authorize(Roles = "user")]
        [HttpGet("total-amount")]
        public async Task<ActionResult<IncomeResTotalAmountDto>> GetAmountsSum()
        {
            // Get userId from Token
            var userId = await getIdTool.GetIdFromToken(HttpContext);

            if (userId == null)
            {
                return Unauthorized("No ID found from token.");
            }

            var amountSum = await service.GetAmountSum(userId.Value);

            return Ok(amountSum);
        }

        // Get incomes with spesific dates
        [Authorize(Roles = "user")]
        [HttpGet("get-incomes-by-date")]
        public async Task<ActionResult<List<IncomeDto>>> GetIncomes(DateTimeReqDto req)
        {
            // Get userId from Token
            var userId = await getIdTool.GetIdFromToken(HttpContext);

            if (userId == null)
            {
                return Unauthorized("No ID found from token.");
            }

            var incomes = await service.GetAllIncomes(req, userId.Value); // Täältä tulee palauttaa Incomes Models

            return mapper.Map<List<IncomeDto>>(incomes);
        }

        // Create income
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult<IncomeDto>> CreateIncome(IncomeDto req)
        {

            var userId = await getIdTool.GetIdFromToken(HttpContext);

            if (userId == null)
            {
                return Unauthorized("No ID found from token.");
            }

            // Tarvittiin userId.Value, koska toolista tuleva id voi olla null jos sitä ei löydy
            var newExpense = await service.CreateIncome(req, userId.Value);

            return Ok(newExpense);

        }


        // Update Income
        [HttpPatch("{id}")]
        public async Task<IncomeDto?> UpdateIncome(IncomeDto req, int id)
        {
            var updatedIncome = await service.UpdateIncome(req, id);

            return updatedIncome;
        }

        // Delete Income
        [HttpDelete("{id}")]
        public async Task<DeleteResDto> DeleteIncomeById(int id)
        {
            var deleteResponse = await service.DeleteIncomeById(id);

            return new DeleteResDto { Message = deleteResponse };
        }
    }
}
