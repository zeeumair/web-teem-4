using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webshop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public byte Image { get; set; }
        public string Description { get; set; }
        public string Category{ get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
