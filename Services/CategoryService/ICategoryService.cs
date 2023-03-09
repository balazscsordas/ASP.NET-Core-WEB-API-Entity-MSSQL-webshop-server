namespace server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories();
        Task<ServiceResponse<ProductOptionsDto>> GetProductOptions(int categoryId);
    }
}
