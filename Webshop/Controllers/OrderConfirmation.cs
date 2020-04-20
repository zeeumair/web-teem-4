using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class OrderConfirmationController : Controller
    {

        private readonly WebshopContext _context;
        private readonly IConfiguration _config;
        private Order newOrder;

        public OrderConfirmationController(WebshopContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IActionResult SelectPaymentAndDeliveryOption(string productId, int orderId, string quantity, string orderItemId, string totalprice)
        {
            ViewBag.totalPrice = totalprice;
            ViewBag.productId = productId;
            ViewBag.orderId = orderId;
            ViewBag.orderItemId = orderItemId;
            ViewBag.quantity = quantity; 

            return View();
        }

    

        public async Task<ActionResult> Confirmation(int orderId)
        {
            var orderItems = await GetOrderItemsByOrder(orderId);
            orderItems.ForEach(o => { o.Order.Confirmed = true; });

            await _context.SaveChangesAsync();

            return View(orderItems);
        }  

        public async Task<ActionResult> DownloadConfirmationPdf(int orderId)
        {
            var dataStream = await GetOrderConfirmationPDF.ViewToString(this, await GetOrderItemsByOrder(orderId, true));
            return File(dataStream, "application/pdf", "OrderConfirmation.pdf");
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrder(int orderId, bool includeConfirmed = false)
        {
            var orderItems = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderId == orderId && o.Order.Confirmed == includeConfirmed);
            return await orderItems.ToListAsync();
        }
    }

}

