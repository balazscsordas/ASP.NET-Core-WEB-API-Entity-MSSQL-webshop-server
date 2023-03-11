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

        private List<string> ConvertStringToList (string value)
        {
            if (value == null)
                return new List<string>();
            var list = value.Split (',').Select (s => s.Trim ()).ToList();
            return list;
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

        public async Task<ServiceResponse<ProductPageProductDataWithOptionsDto>> GetProductDataWithOptions(string productName, int? id, string? size, string? volume)
        {
            var serviceResponse = new ServiceResponse<ProductPageProductDataWithOptionsDto>();
            try
            {
                var dbProducts = await GetProductsByName(productName, size, volume);
                if (dbProducts is null || dbProducts.Count == 0)
                    throw new Exception($"There isn't any product with name: {productName}!");
                var sizeOptions = GetSizeOptions(dbProducts);
                var volumeOptions = GetVolumeOptions(dbProducts);
                var product = dbProducts.First();
                if (id != null)
                {
                    product = dbProducts.First(p => p.Id == id);
                    if (product is null)
                        throw new Exception($"There isn't any product with id: {id} and name: {productName}!");
                }
                var productDataWithOptions = new ProductPageProductDataWithOptionsDto(product, sizeOptions, volumeOptions);
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

        public async Task<ServiceResponse<List<ProductByCategoryIdDto>>> GetProductsByCategoryId(int categoryId, Boolean? onStock, int? minPrice, int? maxPrice, string? sizes, string? volumes)
        {
            var serviceResponse = new ServiceResponse<List<ProductByCategoryIdDto>>();
            try
            {
                var sizesList = ConvertStringToList(sizes);
                var volumesList = ConvertStringToList(volumes);

                var dbProducts = await _context.Products
                    .Where(p => p.CategoryId == categoryId)
                    .Where(p => (onStock == true ? p.CurrentStock > 0 : true == true))
                    .Where(p => (minPrice.HasValue ? p.UnitPrice >= minPrice : true == true))
                    .Where(p => (maxPrice.HasValue ? p.UnitPrice <= maxPrice : true == true))
                    .Where(p => (sizesList.Count > 0 ? sizesList.Contains(p.Size) : true == true))
                    .Where(p => (volumesList.Count > 0 ? volumesList.Contains(p.Volume) : true == true))
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

        public async Task<ServiceResponse<List<ProductByCategoryIdDto>>> GetNewProducts()
        {
            var serviceResponse = new ServiceResponse<List<ProductByCategoryIdDto>>();
            try
            {
                var dbNewProducts = await _context.Products
                    .GroupBy(p => p.Name)
                    .Select(p => p.First())
                    .Take(6)
                    .ToListAsync();
                if (dbNewProducts is null || dbNewProducts.Count == 0) throw new Exception("There isn't any product!");
                serviceResponse.Data = _mapper.Map<List<ProductByCategoryIdDto>>(dbNewProducts);
                serviceResponse.Message = "Successfully fetched 6 product.";
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
