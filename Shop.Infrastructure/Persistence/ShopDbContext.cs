using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<User> Users => Set<User>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureProducts(modelBuilder);
            ConfigureUsers(modelBuilder);
            ConfigureOrders(modelBuilder);
        }

        private static void ConfigureProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(product => product.Name);

            modelBuilder.Entity<Product>()
                .Property(product => product.Price)
                .HasColumnType("decimal(18,2)");
        }

        private static void ConfigureUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();
        }

        private static void ConfigureOrders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(order => order.Items)
                .WithOne()
                .HasForeignKey(orderItem => orderItem.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .Property(order => order.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(orderItem => orderItem.UnitPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}
