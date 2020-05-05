using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Webshop.Models
{
    public class IdentityAppContext : IdentityDbContext<User, AppRole, int>
    {

        //public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public IdentityAppContext(DbContextOptions<IdentityAppContext> options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasKey(oi => new { oi.OrderId, oi.ProductId });
            modelBuilder.Entity<Review>().HasKey(r => new { r.UserId, r.ProductId });


            base.OnModelCreating(modelBuilder);
        }
    }
}