using CloudInvoice.Catalog.Application.DTOs;
using CloudInvoice.Catalog.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudInvoice.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] ProductQueryParameters parameters)
        {
            var result = await _productService.GetProductsAsync(parameters);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productDto)
        {
            var created = await _productService.AddProductAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateDto productDto)
        {
            var updated = await _productService.UpdateProductAsync(id, productDto);
            return updated ? NoContent() : NotFound();
        }

        [HttpGet("{id:guid}/check-availability")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckAvailability(Guid id)
        {
            var result = await _productService.CheckAvailabilityAsync(id);
            return Ok(result);
        }

        [HttpGet("{id:guid}/is-available")]
        [AllowAnonymous]
        public async Task<IActionResult> IsAvailable(Guid id)
        {
            var isAvailable = await _productService.IsAvailableAsync(id);
            return Ok(isAvailable);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _productService.DeleteProductAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        
        [HttpPatch("{id:guid}/toggle-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            var deactivated = await _productService.ToggleStatusAsync(id);
            return deactivated ? NoContent() : NotFound();
        }
    }
}