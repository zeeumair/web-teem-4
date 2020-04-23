using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Webshop.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAdress { get; set; }
        public string PostNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Password { get; set; }
    }
}
