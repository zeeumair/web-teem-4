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
                            context.Users.Where(u => u.Id == 1).First() :
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
                            Name = "airJordans",
                            Price = 100,
                            Image = WebshopContext.ReadFile("Images/airJordans.jpg"),
                            Description = "Fly high like Michael",
                            Category = "sport",
                            CreatedAt = DateTime.Today
                        },
                        Quantity = 1
                    },
                    new OrderItem
                    {
                        Order = new Order
                        {
                            User = context.Users.Any() ?
                            context.Users.Where(u => u.Id == 1).First() :
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
                            Name = "Nike Mercurial Vapor",
                            Price = 100,
                            Image = WebshopContext.ReadFile("Images/NikeMercurialVapor.jpg"),
                            Description = "Play Ball like Messi",
                            Category = "sport",
                            CreatedAt = DateTime.Today
                        },
                        Quantity = 2
                    },
                    new OrderItem
                    {
                        Order = new Order
                        {
                            User = context.Users.Any() ?
                            context.Users.Where(u => u.Id == 1).First() :
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
                            Name = "Nike Air Zoom",
                            Price = 100,
                            Image = WebshopContext.ReadFile("Images/NikeAirZoom.jpg"),
                            Description = "Best running shoe ever",
                            Category = "sport",
                            CreatedAt = DateTime.Today
                        },
                        Quantity = 3
                    });
                }
                context.SaveChanges();
            }
        }
    }
}