using System;

namespace Webshop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte Image { get; set; }
        public string Description { get; set; }
        public string Category{ get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
