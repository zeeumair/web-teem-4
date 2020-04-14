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
            ViewBag.quantity = quantity; 

            return View();
        }

    

        public ActionResult Confirmation(string paymentType, string deliveryTime, double totalPrice, string productId, string orderId, string orderItemId, string quantity)
        {
            List<Product> products = new List<Product>();
            var orderItemContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderId == Int32.Parse(orderId));

      


            foreach (var item in orderItemContext.ToList())
            {
                products.Add(item.Product);
 
                
            }

            var orderContext = _context.Orders.Find(Int32.Parse(orderItemId));
            orderContext.Confirmed = true; 
             _context.SaveChangesAsync();

            ViewBag.orderConfirmationNumber = orderContext.Id;
            ViewBag.delivery = deliveryTime;
            ViewBag.paymentType = paymentType; 
            ViewBag.totalPrice = totalPrice;
            ViewBag.productId = productId;
            ViewBag.orderId = orderId;
            ViewBag.quantity = quantity;
            ViewBag.orderItemId = orderItemId;




            return View(products);
        }  
    }
}

