
namespace server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) 
        { 
            
        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems=> Set<OrderItem>();
        public DbSet<PaymentOption> PaymentOptions => Set<PaymentOption>();
        public DbSet<ShippingData> ShippingDetails=> Set<ShippingData>();
        public DbSet<BillingData> BillingDetails=> Set<BillingData>();
    }
}
