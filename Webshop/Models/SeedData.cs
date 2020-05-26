using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Webshop.Models;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Webshop
{
    public static class SeedData
    {

        public static byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes 
            //to read from file.
            //In this case we want to read entire file. 
            //So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);

            return data;
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = new IdentityAppContext(serviceProvider.GetRequiredService<DbContextOptions<IdentityAppContext>>());
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            var user = new User
            {
                FirstName = "Test",
                LastName = "User",
                Password = "!1Aaaa",
                StreetAdress = "Gogubbegatan 3",
                PostNumber = "41706",
                City = "Gothenburg",
                Country = "Sweden",
                Email = "omgzshoezz@gmail.com",
                Currency = "SEK",
                PhoneNumber = "0700000000",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "omgzshoezz@gmail.com"
            };
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, "!1Aaaa");
            var manager = serviceProvider.GetRequiredService<UserManager<User>>();
            manager.CreateAsync(user).Wait();
            
            var products = new List<Product> { 
                new Product
                {
                    Name = "Air Jordans",
                    Price = 199,
                    Image = ReadFile("Images/airJordans.jpg"),
                    Description = "Fly high like Michael",
                    Category = "sport",
                    CreatedAt = DateTime.Today
                },
                new Product
                {
                    Name = "Nike Air Zoom",
                    Price = 99,
                    Image = ReadFile("Images/NikeAirZoom.jpg"),
                    Description = "Zoom like the nikon 500",
                    Category = "sport",
                    CreatedAt = DateTime.Today
                },
                new Product
                {
                    Name = "Nike Mercurial Vapor",
                    Price = 150,
                    Image = ReadFile("Images/NikeMercurialVapor.jpg"),
                    Description = "Play Ball like Messi",
                    Category = "sport",
                    CreatedAt = DateTime.Today
                },                
                new Product
                {
                    Name = "Moon Boots",
                    Price = 299,
                    Image = ReadFile("Images/moonboot.jpg"),
                    Description = "Perfect for netflix and fake the moon landing",
                    Category = "Space Exploration",
                    CreatedAt = DateTime.Today
                },                
                new Product
                {
                    Name = "Pimp shoe Fish-tank edt",
                    Price = 100,
                    Image = ReadFile("Images/pimpshoe.jpg"),
                    Description = "When Paolo comes a knocking better have shoes with style",
                    Category = "Playah",
                    CreatedAt = DateTime.Today
                },                
                new Product
                {
                    Name = "Croc-Martens",
                    Price = 999,
                    Image = ReadFile("Images/croc-martens.jpg"),
                    Description = "The perfect shoe for the casual emo",
                    Category = "Casual",
                    CreatedAt = DateTime.Today
                },                
                new Product
                {
                    Name = "Clown Shoes",
                    Price = 1,
                    Image = ReadFile("Images/clownshoes.jpg"),
                    Description = "When Insane Clown Posse is your life, strut your stuff with these flippin clown shoes",
                    Category = "Juggalo",
                    CreatedAt = DateTime.Today
                }
            };

            var order = new Order
            {
                User = context.Users.FirstOrDefault(),
                PaymentOption = "Swish",
                TotalAmount = 300,
                DeliveryOption = "2-5 days"
            };

            var orderItems = new List<OrderItem>();
            products.ForEach(product => {
                orderItems.Add(
                    new OrderItem
                    {
                        Order = order,
                        Product = product,
                        Quantity = 1
                    });
            });
            context.OrderItems.AddRange(orderItems);

            var currencyRates = CurrencyManager.GetCurrencyRates().Result;
            foreach (KeyValuePair<string, double> item in  currencyRates.Rates)
            {
                context.Currencies.Add(new Currency
                {
                    CurrencyCode = item.Key,
                    CurrencyRate = item.Value,
                    LastUpdated = DateTimeOffset.UtcNow
                });
            }
            context.SaveChanges();
            context.Dispose();
        }
    }
}