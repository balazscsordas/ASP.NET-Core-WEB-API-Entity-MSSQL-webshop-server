using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public int UnitPrice { get; set; }
        public int CurrentStock { get; set; }
        public string Description { get; set; } 
        public int CategoryId { get; set; }
        public string? Size { get; set; }
        public string? Volume { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
