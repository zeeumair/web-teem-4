using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult SelectPaymentAndDeliveryOption(string productId, string orderId, string quantity, string orderItemId)
        {
            if (productId == null)
            {
                productId = "1"; 
            }

            var context = _context.Products.Find(Int32.Parse(productId));

            decimal totalPrice = context.Price * Int32.Parse(quantity);

            ViewBag.totalPrice = totalPrice;
            ViewBag.productId = productId;
            ViewBag.orderId = orderId;
            ViewBag.orderItemId = orderItemId;

            return View();
        }

    

        public async Task<IActionResult> Confirmation(string paymentType, string deliveryTime, double totalPrice, string productId, string orderId, string orderItemId)
        {

            List<Product> products = new List<Product>();

            var orderContext = _context.Orders.Find(Int32.Parse(orderItemId));
            orderContext.Confirmed = true; 
            await _context.SaveChangesAsync();

            var orderItemContext = _context.OrderItems.Find(Int32.Parse(orderItemId), Int32.Parse(orderId));

            products.Add(orderItemContext.Product); 

            ViewBag.products = products;
            ViewBag.delivery = deliveryTime;
            ViewBag.totalPrice = totalPrice;
            ViewBag.orderConfirmationNumber = orderContext.Id;



            return View(products);
        }
    }
}

