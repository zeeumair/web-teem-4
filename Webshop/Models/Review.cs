using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class Review
    {
        [Key, Column(Order = 0)]
        public virtual User User { get; set; }

        [Key, Column(Order = 1)]
        public virtual Product Product { get; set; }

        public int Stars { get; set; }
        public string Description { get; set; }

    }
}
