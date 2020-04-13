using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Webshop.Data;
using System;
using System.Linq;
using Webshop.Models;

namespace Webshop
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WebshopContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<WebshopContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }

                context.Users.AddRange(
                    new User {
                        FirstName = "Test",
                        LastName = "User",
                        Username = "testuser1",
                        Password = "password",
                        StreetAdress = "Gogubbegatan 3",
                        PostNumber = "41706",
                        City = "Gothenburg",
                        Country = "Sweden",
                        Email = "test@testuser.com",
                        Currency = "SEK",
                        PhoneNumber = "0700000000"                       
                    }
                );
                context.SaveChanges();
            }
        }
    }
}