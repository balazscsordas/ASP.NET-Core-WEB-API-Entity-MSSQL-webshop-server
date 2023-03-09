namespace server.Dtos.Product
{
    public class ProductByCategoryIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public int UnitPrice { get; set; }
    }
}
