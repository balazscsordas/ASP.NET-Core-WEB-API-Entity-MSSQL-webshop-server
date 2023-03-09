using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> Get()
        {
            return Ok(await _categoryService.GetAllCategories());
        }

        [HttpGet("GetProductOptions")]
        public async Task<ActionResult<ServiceResponse<ProductOptionsDto>>> GetProductOptions(int categoryId)
        {
            return Ok(await _categoryService.GetProductOptions(categoryId));
        }
    }
}
