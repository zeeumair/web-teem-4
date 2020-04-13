using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webshop.Models
{
    public class OrderItem
    {
        [Key, Column(Order = 0)]
        public virtual Order Order { get; set; }
        [Key, Column(Order = 1)]
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
