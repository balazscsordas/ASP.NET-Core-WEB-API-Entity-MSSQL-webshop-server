    namespace server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ProductService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        private async Task<List<ProductPageDto>> GetProductsByName(string productName, string? size, string? volume)
        {
            var dbProducts = await _context.Products.Where(p => 
            (
                p.Name == productName 
                && (size == null || p.Size == size)
                && (volume == null || p.Volume == volume)
            )).ToListAsync();
            return _mapper.Map<List<ProductPageDto>>(dbProducts);
        }

        private List<string> GetSizeOptions(List<ProductPageDto> productList)
        {
            List<string> sizeOptions = new List<string>();
            productList.ForEach(p =>
            {
                if (!sizeOptions.Contains(p.Size) && p.Size != null)
                {
                    sizeOptions.Add(p.Size);
                }
            });

            return sizeOptions;
        }

        private List<string> GetVolumeOptions(List<ProductPageDto> productList)
        {
            List<string> volumeOptions = new List<string>();
            productList.ForEach(p =>
            {
                if (!volumeOptions.Contains(p.Volume) && p.Volume != null)
                {
                    volumeOptions.Add(p.Volume);
                }
            });

            return volumeOptions;
        }

        public async Task<ServiceResponse<ProductPageProductDataWithOptionsDto>> GetProductDataWithOptions(string productName, string? size, string? volume)
        {
            var serviceResponse = new ServiceResponse<ProductPageProductDataWithOptionsDto>();
            try
            {
                var dbProducts = await GetProductsByName(productName, size, volume);
                if (dbProducts is null || dbProducts.Count == 0)
                    throw new Exception($"There isn't any product with name: {productName}!");
                var sizeOptions = GetSizeOptions(dbProducts);
                var volumeOptions = GetVolumeOptions(dbProducts);
                var productDataWithOptions = new ProductPageProductDataWithOptionsDto(dbProducts.First(), sizeOptions, volumeOptions);
                serviceResponse.Data = productDataWithOptions;
                serviceResponse.Message = "Successfully fetched the product details and the available options.";
            } 
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ProductByCategoryIdDto>>> GetProductsByCategoryId(int categoryId)
        {
            var serviceResponse = new ServiceResponse<List<ProductByCategoryIdDto>>();
            try
            {
                var dbProducts = await _context.Products
                    .Where(p => p.CategoryId == categoryId)
                    .GroupBy(p => p.Name)
                    .Select(p => p.First())
                    .ToListAsync();
                if (dbProducts is null || dbProducts.Count == 0) throw new Exception($"There isn't any product in category with Id: {categoryId}!");
                serviceResponse.Data = _mapper.Map<List<ProductByCategoryIdDto>>(dbProducts);
                serviceResponse.Message = "Successfully fetched the products of category.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
