using API.Data.Dtos;
using API.Data.Dtos.Incomes;
using API.Models;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController(IIncomesService service, IMapper mapper) : ControllerBase
    {
        // Get Amount-columns sum
        [HttpGet("total-amount")]
        public async Task<ActionResult<IncomeResTotalAmountDto>> GetAmountsSum(int userId)
        {
            var amountSum = await service.GetAmountSum(userId);
            return Ok(amountSum);
        }

        // Get incomes with spesific dates
        [HttpGet("get-incomes-by-date")]
        public async Task<List<IncomeDto>> GetIncomes(DateTimeReqDto req)
        {
            var incomes = await service.GetAllIncomes(req); // Täältä tulee palauttaa Incomes Models
            return mapper.Map<List<IncomeDto>>(incomes);
                //mapper.Map<List<IncomeDto>>(incomes)
                //incomes
             // Tämä taas palauttaa incomeDto-listana
            // mapper.Map<UserResDto>(user)
        }


        // Update Income
        [HttpPatch]
        public async Task<IncomeDto> UpdateIncome(IncomeDto req)
        {
            var updatedIncome = await service.UpdateIncome(req);

            return updatedIncome;
        }



        // Delete Income
        [HttpDelete]
        public async Task<string?> DeleteIncomeById(int incomeId)
        {
            var deleteResponse = await service.DeleteIncomeById(incomeId);

            return deleteResponse;
        }



    }

}
