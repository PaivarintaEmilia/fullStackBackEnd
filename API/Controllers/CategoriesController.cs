using System.Security.Claims;
using API.Data.Dtos;
using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService service) : ControllerBase
    {

        // Categorian lisäys | vain adminit 
        [Authorize(Roles = "admin")]
        [HttpPost("addCategory")]
        public async Task<ActionResult<CategoryResDto>> CreateCategory(CategoryReqDto req)
        {
            // Debug
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }

            // Hae käyttäjän ID
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
            Console.WriteLine($"userIdClaim: {userIdClaim}");

            if (userIdClaim == null)
            {
                return Unauthorized("User ID ei löytynyt tokenista.");
            }

            var userId = int.Parse(userIdClaim.Value);
            var newCategory = await service.Create(req, userId);

            return Ok(newCategory);
        }

        // Kategorian muokkaaminen
        [Authorize(Roles = "admin")]
        [HttpPatch("{id}")] // Vain osittaiseen muokkaukseen
        public async Task<ActionResult<CategoryResDto?>> UpdateCategory(CategoryReqDto req, int id)
        {
            // En ottanut id:tä mukaan, koska en tiedä onko sillä väliä. Riippuu siitä halutaanko pitää tieto categorian luojasta tietokannasta vai 
            // viimeisimmästä muokkaajasta
            var updatedCategory = await service.Update(req, id);
            return Ok(updatedCategory);
        }

        // Kategorian poisto
        [Authorize("admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string?>> DeleteCategory(int id)
        {
            var deletetion = await service.Delete(id);
            return Ok(deletetion);
        }

    }
}
