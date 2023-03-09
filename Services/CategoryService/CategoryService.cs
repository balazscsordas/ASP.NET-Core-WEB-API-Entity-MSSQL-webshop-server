

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

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            var dbCategories = await _context.Categories.ToListAsync();
            serviceResponse.Data = dbCategories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList();
            serviceResponse.Message = "Successfully fetched all of the categories!";
            return serviceResponse;
        }
    }
}
