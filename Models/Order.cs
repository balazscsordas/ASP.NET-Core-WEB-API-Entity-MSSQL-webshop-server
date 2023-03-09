using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public BillingData BillingData { get; set; }
        public ShippingData ShippingData { get; set; }
        public PaymentOption PaymentOption { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
