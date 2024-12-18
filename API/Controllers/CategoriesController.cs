using System.Security.Claims;
using API.Data.Dtos;
using API.Data.Dtos.Categories;
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
    public class CategoriesController(ICategoriesService service, IGetIdTool getIdTool, IMapper mapper) : ControllerBase
    {

        // Get categories
        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<ActionResult<List<CategoriesResDto>>> GetCategories()
        {

            // Tarvitaan id, jotta voidaan hakea käyttäjän luomat categoriat
            var userId = await getIdTool.GetIdFromToken(HttpContext);

            if (userId == null)
            {
                return Unauthorized("No ID found from token.");
            }

            // Palauttaa modelit
            var categories = await service.GetCategories(userId.Value);

            //return null;
            return mapper.Map<List<CategoriesResDto>>(categories);

        }

        // // Categorian lisäys | vain adminit 
        // [Authorize(Roles = "admin")]
        // [HttpPost("addCategory")]
        // public async Task<ActionResult<CategoryResDto>> CreateCategory(CategoryReqDto req)
        // {

        //     // Hae käyttäjän ID tokenista
        //     var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

        //     if (userIdClaim == null)
        //     {
        //         return Unauthorized("User ID ei löytynyt tokenista.");
        //     }

        //     // Muutetaan id kokonaisluvuksi
        //     var userId = int.Parse(userIdClaim.Value);

        //     var newCategory = await service.Create(req, userId);

        //     return Ok(newCategory);
        // }

        // // Kategorian muokkaaminen | vain adminit 
        // [Authorize(Roles = "admin")]
        // [HttpPatch("{id}")] // Vain osittaiseen muokkaukseen
        // public async Task<ActionResult<CategoryResDto?>> UpdateCategory(CategoryReqDto req, int id)
        // {
        //     // En ottanut id:tä mukaan, koska en tiedä onko sillä väliä. Riippuu siitä halutaanko pitää tieto categorian luojasta tietokannasta vai 
        //     // viimeisimmästä muokkaajasta. Tästä löytyy lisätietoa ReadMe-filusta. 
        //     var updatedCategory = await service.Update(req, id);
        //     return Ok(updatedCategory);
        // }

        // // Kategorian poisto | vain adminit 
        // [Authorize("admin")]
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<string?>> DeleteCategory(int id)
        // {
        //     var deletetion = await service.Delete(id);
        //     return Ok(deletetion);
        // }

    }
}
