using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class ShippingData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
