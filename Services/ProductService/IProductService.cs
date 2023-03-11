global using server.Dtos.Product;

namespace server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<ProductByCategoryIdDto>>> GetProductsByCategoryId(int categoryId, Boolean? onStock, int? minPrice, int? maxPrice, string? sizes, string? volumes);
        Task<ServiceResponse<ProductPageProductDataWithOptionsDto>> GetProductDataWithOptions(string productName, int? id, string? size, string? volume);
        Task<ServiceResponse<List<ProductByCategoryIdDto>>> GetNewProducts();
    }
}
