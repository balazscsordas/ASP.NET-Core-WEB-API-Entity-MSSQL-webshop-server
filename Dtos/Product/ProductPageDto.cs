namespace server.Dtos.Product
{
    public class ProductPageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public int UnitPrice { get; set; }
        public int CurrentStock { get; set; }
        public string Description { get; set; } 
        public string Size { get; set; }
        public string Volume { get; set; }
    }

    public class ProductPageProductDataWithOptionsDto
    {
        public ProductPageDto ProductData { get; set; }
        public List<string> SizeOptions { get; set; }
        public List<string> VolumeOptions { get; set; }

        public ProductPageProductDataWithOptionsDto(ProductPageDto productData, List<string> sizeOptions, List<string> volumeOptions)
        {
            ProductData = productData;
            SizeOptions = sizeOptions;
            VolumeOptions = volumeOptions;
        }
    }
}
