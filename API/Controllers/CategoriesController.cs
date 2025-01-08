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
        [Authorize(Roles = "admin,user")] 
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

        // Create a new cateogry
        [Authorize(Roles = "admin,user")] 
        [HttpPost]
        public async Task<ActionResult<CategoriesResDto>> CreateCategory(CategoriesReqDto req)
        {
            var userId = await getIdTool.GetIdFromToken(HttpContext);

            if (userId == null)
            {
                return Unauthorized("No ID found from token.");
            }

            var newCategory = await service.CreateCategory(req, userId.Value);

            return new CategoriesResDto { Id = newCategory.Id, Name = newCategory.Name };
        }

        // Update category
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<CategoriesResDto?> UpdateCategory(CategoriesReqDto req, int id)
        {
            var updatedCategory = await service.UpdateCategory(req, id);

            return updatedCategory;
        }

        // Delete category
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<DeleteResDto> DeleteCategory(int id)
        {
            var deleteResponse = await service.DeleteCategory(id);

            return new DeleteResDto { Message = deleteResponse };
        }
    }
}
