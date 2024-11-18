using CatalogService.Helpers;
using CatalogService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;

        private readonly ProductsService _productsService;

        public ProductsController(ProductsService productsService, ILogger<ProductsController> logger)
        {
            _productsService = productsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ProductResponse.GetIndex> Get([FromQuery] ProductRequest.Index request)
        {
            var response = await _productsService.GetAsync(request);
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductRequest.Create request)
        {
            var response = await _productsService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = response.ProductId });
        }

    }
}
