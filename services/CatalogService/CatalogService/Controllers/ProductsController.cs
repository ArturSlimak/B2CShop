using CatalogService.DTOs;
using CatalogService.Helpers;
using CatalogService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ProductsService _productsService;

        public ProductsController(ProductsService productsService) =>
            _productsService = productsService;

        [HttpGet]
        public async Task<List<ProductDTO.Index>> Get([FromQuery] ProductRequest.Index request) => await _productsService.GetAsync(request);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductRequest.Create request)
        {
            var createdProduct = await _productsService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProduct);
        }

    }
}
