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

        public IActionResult SelectPaymentAndDeliveryOption(IQueryable<OrderItem> orderItem)
        {
            // Realy price has to be generated trough varukorg, and products 

            decimal totalPrice = 0; 

            foreach (var product in orderItem)
            {
                totalPrice = product.Product.Price * Convert.ToDecimal(product.Quantity);
            };

            ViewBag.OrderItems = orderItem;
            ViewBag.totalPrice = totalPrice;

            return View();
        }

    

        public async Task<IActionResult> Confirmation(string paymentType, string deliveryTime, double totalPrice, IQueryable<OrderItem> orderItem)
        {
            

            //newOrder = new Order
            //{
            //    //Need a User sendt through the params
            //    User = user,
            //    PaymentOption = paymentType,
            //    TotalAmount = totalPrice,
            //    DeliveryOption = deliveryTime,
            //    CreatedAt = DateTime.Today
            //};

            ////aktivera senare
            //_context.Orders.Add(newOrder);
            //await _context.SaveChangesAsync();

            ViewBag.payment = paymentType;
            ViewBag.delivery = deliveryTime;
            ViewBag.totalPrice = totalPrice;
            ViewBag.orderItem = orderItem; 

            return View();
        }
    }
}

