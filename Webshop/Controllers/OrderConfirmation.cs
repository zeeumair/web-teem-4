using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class OrderConfirmationController : Controller
    {

        private readonly WebshopContext _context;
        private Order newOrder;

        public OrderConfirmationController(WebshopContext context)
        {
            _context = context;
        }

        public IActionResult SelectPaymentAndDeliveryOption(User user, List<Product> products)
        {
            // Realy price has to be generated trough varukorg, and products 
            return View();
        }

    

        public async Task<IActionResult> Confirmation(string paymentType, string deliveryTime, User user, List<Product> products)
        {

            

            //// Only for test purpose before real products is is sendt through parameters. 

            //List<Product> productList = new List<Product>();  

            //var product = new Product
            //{
            //    Name = "airJordans",
            //    Price = 100,
            //    Image = ReadFile("Images/airJordans.jpg"),
            //    Description = "Fly high like Michael",
            //    Category = "sport", 
            //    CreatedAt = DateTime.Today
            //};

            //productList.Add(product); 


            // Only for test purpose before real user is is sendt through parameters. 
            //var user = new User
            //{

            //    FirstName = "petter",
            //    LastName = "Fagerlund",
            //    Username = "petter",
            //    Password = "123",
            //    StreetAdress = "runbäcksgatan",
            //    PostNumber = "123",
            //    City = "göteborg",
            //    Country = "Sweden",
            //    Email = "petter@gmail.com",
            //    Currency = "Sek",
            //    CreatedAt = DateTime.Today,
            //    PhoneNumber = "0709556644"
            //};
            ////////////////////////////////////////

            //newOrder = new Order
            //{
            //    User = user,
            //    PaymentOption = paymentType,
            //    TotalAmount = Convert.ToDouble(price),
            //    DeliveryOption = deliveryTime,
            //    CreatedAt = DateTime.Today
            //};

            //aktivera senare 
            //_context.Orders.Add(newOrder);
            //await _context.SaveChangesAsync();

            ViewBag.payment = paymentType;
            ViewBag.delivery = deliveryTime;
            //ViewBag.price = price;

            return View();
        }
    }
}

