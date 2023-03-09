namespace server.Dtos.Order
{
    public class ProductFromOrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public int UnitPrice { get; set; }
        public int CurrentStock { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
        public string? Volume { get; set; }
    }
}
