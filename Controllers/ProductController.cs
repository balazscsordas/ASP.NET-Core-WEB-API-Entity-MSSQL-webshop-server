using Microsoft.AspNetCore.Mvc;
using server.Services.ProductService;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetProductDataByName")]
        public async Task<ActionResult<ServiceResponse<ProductPageProductDataWithOptionsDto>>> GetProductDataWithOptions(string productName, int? id, string? size, string? volume)
        {
            return Ok(await _productService.GetProductDataWithOptions(productName, id, size, volume));
        }

        [HttpGet("GetProductsByCategoryId")]
        public async Task<ActionResult<ServiceResponse<ProductPageDto>>> GetProductsByCategoryId(int id, Boolean? onStock, int? minPrice, int? maxPrice, string? sizes, string? volumes)
        {
            return Ok(await _productService.GetProductsByCategoryId(id, onStock, minPrice, maxPrice, sizes, volumes));
        }

        [HttpGet("GetNewProducts")]
        public async Task<ActionResult<ServiceResponse<List<ProductByCategoryIdDto>>>> GetNewProducts()
        {
            return Ok(await _productService.GetNewProducts());
        }
    }
}
