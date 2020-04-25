using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Webshop.Data;
using System;
using System.Linq;
using Webshop.Models;
using System.IO;
using System.Collections.Generic;

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
            using (var context = new WebshopContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<WebshopContext>>()))
            {

                if (context.Users.Any() && context.OrderItems.Any() && context.Currencies.Any())
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
                            Image = ReadFile("Images/airJordans.jpg"),
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
                            Image = ReadFile("Images/NikeMercurialVapor.jpg"),
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
                            Name = "airJordans",
                            Price = 100,
                            Image = ReadFile("Images/airJordans.jpg"),
                            Description = "Fly high like Michael",
                            Category = "sport",
                            CreatedAt = DateTime.Today
                        },
                        Quantity = 3
                    });
                }
                if(!context.Currencies.Any())
                {
                    var currencyRates = CurrencyManager.GetCurrencyRates().Result;
                    foreach (KeyValuePair<string, double> item in  currencyRates.Rates)
                    {
                        context.Currencies.Add(new Currency
                        {
                            CurrencyCode = item.Key,
                            CurrencyRate = item.Value
                        });
                    }
                }
                context.SaveChanges();
            }
        }
    }
}