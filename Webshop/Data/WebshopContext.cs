using Microsoft.EntityFrameworkCore;
using Webshop.Models;

namespace Webshop.Data
{
    public class WebshopContext : DbContext
    {
        public WebshopContext(DbContextOptions<WebshopContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}