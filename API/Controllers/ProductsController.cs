using API.Data.Dtos;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService service, IMapper mapper) : ControllerBase
    {
        
        // RouteHandler kaikkien tuotteiden hakua varten
        [HttpGet]
        public async Task<ActionResult<List<ProductResDto>>> GetAllProducts()
        {
            var products = await service.GetAll();
            return Ok(
                mapper.Map<List<ProductResDto>>(products)
            );
        }

        // RouteHandler tuotteiden hakuun kategorioittain
        // Ottaa vastaa kategorian nimen ja palauttaa listan tuotteita, jotka kuuluvat tähän kategoriaan
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<List<ProductResDto>>> GetProductsByCategory(int categoryId)
        {
            var products = await service.GetByCategory(categoryId);
            return Ok(
                mapper.Map<List<ProductResDto>>(products)
            );
        }

    }
}
