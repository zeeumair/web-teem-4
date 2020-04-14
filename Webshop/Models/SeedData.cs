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
                if (context.Users.Any() && context.OrderItems.Any())
                {
                    return;
                }

                if (!context.OrderItems.Any())
                {
                    context.OrderItems.AddRange(
                    new OrderItem
                    {
                        Order = new Order
                        {
                            User = context.Users.Any() ?
                            context.Users.Where(u => u.Id == 1).FirstOrDefault() :
                            new User
                            {
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
                            },
                            PaymentOption = "Swish",
                            TotalAmount = 11.11,
                            DeliveryOption = "Express"
                        },
                        Product = new Product
                        {
                            Name = "TestProduct",
                            Price = 73.57m,
                            Image = 1,
                            Description = "OMGZ-iest of shoes",
                            Category = "TestCategory",
                        },
                        Quantity = 5,
                    });
                }
                context.SaveChanges();
            }
        }
    }
}