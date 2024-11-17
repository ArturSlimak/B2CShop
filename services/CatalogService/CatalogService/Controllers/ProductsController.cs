using CatalogService.DTOs;
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
        public async Task<List<ProductDTO.Index>> Get() => await _productsService.GetAsync();

        [HttpPost]
        public async Task<IActionResult> Post(ProductDTO.Create newProduct)
        {
            var createdProduct = await _productsService.CreateAsync(newProduct);
            return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProduct);
        }

    }
}
