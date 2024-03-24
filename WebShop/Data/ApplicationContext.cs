using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShop.Models;

namespace WebShop.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });

            modelBuilder.Entity<OrderProduct>()
                .HasKey(pc => new { pc.OrderId, pc.ProductId });
            modelBuilder.Entity<OrderProduct>()
                .HasOne(p => p.Order)
                .WithMany(pc => pc.OrderProducts)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderProduct>()
                .HasOne(p => p.Product)
                .WithMany(pc => pc.OrderProducts)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CartProduct>()
                .HasKey(pc => new { pc.CartId, pc.ProductId });
            modelBuilder.Entity<CartProduct>()
                .HasOne(p => p.Cart)
                .WithMany(pc => pc.CartProducts)
                .HasForeignKey(p => p.CartId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CartProduct>()
                .HasOne(p => p.Product)
                .WithMany(pc => pc.CartProducts)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
