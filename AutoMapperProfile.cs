namespace server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PaymentOption, PaymentOptionDto>();
            CreateMap<Category, GetCategoryDto>();
            CreateMap<Product, ProductPageDto>();
            CreateMap<Product, ProductByCategoryIdDto>();
            CreateMap<Product, ProductPageProductDataWithOptionsDto>();
            CreateMap<ShippingDataDto, ShippingData>();
            CreateMap<BillingDataDto, BillingData>();
        }
    }
}
 