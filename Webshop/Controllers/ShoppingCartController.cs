using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly WebshopContext _context;

        public ShoppingCartController(WebshopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var webshopContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product)/*.Where(u => !u.Order.Confirmed && u.Order.User.Id == 1)*/; // Add filter on current User once we have a user login system

            var orderItems = await webshopContext.ToListAsync();
            
            var productId = "";
            var orderId = "";
            var quantity = "";
            var orderItemId = "";

            foreach (var item in orderItems)
            {
                productId = productId + "," + item.Product.Id.ToString();
                orderId = orderId + "," + item.Order.Id.ToString();
                quantity = quantity + "," + item.Quantity.ToString();
                orderItemId = orderItemId + "," + item.OrderId.ToString(); 
            }

            ViewBag.productId = productId;
            ViewBag.orderId = orderId;
            ViewBag.quantity = quantity;
            ViewBag.orderItemId = orderItemId;

            return View(orderItems);
        }

        public async Task<IActionResult> AddProductToCart(int id)
        {
            var userId = 1;
            if (OrderItemExists(userId, id))
            {
                var orderItem = _context.OrderItems.Where(oi => oi.Order.User.Id == userId && oi.ProductId == id && !oi.Order.Confirmed).FirstOrDefault();
                orderItem.Quantity += 1;
                await _context.SaveChangesAsync();
            }
            else
            {
                var product = await _context.Products.FindAsync(id);
                var order = await _context.Orders.Where(o => o.User.Id == userId && !o.Confirmed).FirstOrDefaultAsync();
                var newOrderItem = _context.OrderItems.Add(
                    new OrderItem
                    {
                        Order = order ?? new Order
                        {
                            User = await _context.Users.FindAsync(userId),
                            PaymentOption = "",
                            TotalAmount = 0,
                            DeliveryOption = ""
                        },
                        Product = product,
                        Quantity = 1
                    });
            }
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> DecrementProductQuantity(int? id, int? idPart)
        {
            var currentItems = await _context.OrderItems.FindAsync(id, idPart);
            if(currentItems.Quantity <= 1)
            {
                _context.OrderItems.Remove(currentItems);
            }
            else
            {
                currentItems.Quantity -= 1;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> IncrementProductQuantity(int? id, int? idPart)
        {
            var currentItems = await _context.OrderItems.FindAsync(id, idPart);
            currentItems.Quantity += 1;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemExists(int id, int idPart)
        {
            return _context.OrderItems.Any(e => e.OrderId == id && e.ProductId == idPart && !e.Order.Confirmed);
        }
    }
}
