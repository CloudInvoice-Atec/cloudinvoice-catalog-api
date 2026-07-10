using CloudInvoice.Catalog.Application.DTOs;
using CloudInvoice.Catalog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudInvoice.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productDto)
        {
            await _productService.AddProductAsync(productDto);
            return Ok();
        }
    }
}
