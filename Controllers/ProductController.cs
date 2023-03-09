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

        [HttpGet("getProductDataByName")]
        public async Task<ActionResult<ServiceResponse<ProductPageProductDataWithOptionsDto>>> GetProductDataWithOptions(string productName, string? size, string? volume)
        {
            return Ok(await _productService.GetProductDataWithOptions(productName, size, volume));
        }

        [HttpGet("getProductsByCategoryId")]
        public async Task<ActionResult<ServiceResponse<ProductPageDto>>> GetProductsByCategoryId(int categoryId)
        {
            return Ok(await _productService.GetProductsByCategoryId(categoryId));
        }
    }
}
