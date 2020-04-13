using System.ComponentModel.DataAnnotations.Schema;

namespace Webshop.Models
{
    public class Review
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public int Stars { get; set; }
        public string Description { get; set; }
    }
}
