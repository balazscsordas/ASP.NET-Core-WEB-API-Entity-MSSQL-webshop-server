global using server.Dtos.Product;

namespace server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<ProductByCategoryIdDto>>> GetProductsByCategoryId(int categoryId);
        Task<ServiceResponse<ProductPageProductDataWithOptionsDto>> GetProductDataWithOptions(string productName, string? size, string? volume);
        Task<ServiceResponse<List<ProductByCategoryIdDto>>> GetNewProducts();
    }
}
