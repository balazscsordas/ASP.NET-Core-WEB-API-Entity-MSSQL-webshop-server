namespace server.Dtos.Order
{
    public class ShippingDataDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string StreetAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string? AptSuiteUnit { get; set; }
    }
}
