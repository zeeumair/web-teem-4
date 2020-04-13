using System;
using System.ComponentModel.DataAnnotations;

namespace Webshop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string StreetAdress { get; set; }
        public string PostNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
