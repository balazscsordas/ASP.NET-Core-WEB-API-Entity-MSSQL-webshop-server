

using server.Models;

namespace server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CategoryService(IMapper mapper, DataContext context) 
        {
            _context = context;
            _mapper = mapper;
        }

        private class ProductOptions
        {
            public List<string> SizeOptions { get; set; } = new List<string>();
            public List<string> VolumeOptions { get; set; } = new List<string>();

            public ProductOptions(List<Product> products)
            {
                foreach (Product product in products)
                {
                    if (product.Size != null && !SizeOptions.Contains(product.Size))
                    {
                        SizeOptions.Add(product.Size);
                    }
                    if (product.Volume != null && !VolumeOptions.Contains(product.Volume))
                    {
                        VolumeOptions.Add(product.Volume);
                    }
                }
            }
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            var dbCategories = await _context.Categories.ToListAsync();
            serviceResponse.Data = dbCategories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList();
            serviceResponse.Message = "Successfully fetched all of the categories!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<ProductOptionsDto>> GetProductOptions(int categoryId)
        {
            var serviceResponse = new ServiceResponse<ProductOptionsDto>();
            try
            {
                var dbProducts = await _context.Products
                    .Where(p => p.CategoryId == categoryId)
                    .ToListAsync();
                if (dbProducts is null || dbProducts.Count == 0) throw new Exception($"There isn't any product with categoryId: {categoryId}");
                var productOptions = new ProductOptions(dbProducts);
                var laci = new ProductOptionsDto();
                laci.SizeOptions = productOptions.SizeOptions;
                laci.VolumeOptions= productOptions.VolumeOptions;

                serviceResponse.Data = laci;
                serviceResponse.Message = $"Successfully fetched all of the product options for categoryId: {categoryId}";
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
