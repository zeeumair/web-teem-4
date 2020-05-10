using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Webshop.Models
{
    public class AppRole : IdentityRole<int>
    {
        [Required]
        public string RoleName { get; set; }
    }
}
