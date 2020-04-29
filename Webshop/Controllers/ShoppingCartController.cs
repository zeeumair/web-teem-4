using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IdentityAppContext _context;
        private IQueryable<OrderItem> webshopContext;
        private List<string> orderItems;
        private List<OrderItem> orderItemToList;

        public ShoppingCartController(IdentityAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<OrderItem> webshopContext = null;
            var UnconfirmedOrderItemsUser = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Include(ou => ou.Order.User).Where(u => !u.Order.Confirmed && u.Order.User.Email == User.Identity.Name); // Add filter on current User once we have a user login system

            if (User.Identity.IsAuthenticated && UnconfirmedOrderItemsUser != null)
            {

                    webshopContext = UnconfirmedOrderItemsUser;
               
            } 
            if(User.Identity.IsAuthenticated || !User.Identity.IsAuthenticated)
            {
                webshopContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(u => !u.Order.Confirmed);

            }
           

            orderItems = new List<string>();
            orderItemToList = await webshopContext.ToListAsync();

            foreach (var item in webshopContext)
            {               
                orderItems.Add(item.OrderId.ToString());
            }

            TempData["OrderItems"] = orderItems;

            return View(orderItemToList);
        }

        public async Task<IActionResult> AddProductToCart(int id, string userEmail)
        {
            if (OrderItemExists(userEmail, id))
            {
                var orderItem = _context.OrderItems.Where(oi => oi.Order.User.Email == userEmail && oi.ProductId == id && !oi.Order.Confirmed).FirstOrDefault();

                if (orderItem != null)
                {
                    orderItem.Quantity += 1;
                    await _context.SaveChangesAsync();
                }
                
            }
            else
            {
                var product = await _context.Products.FindAsync(id);
               //var order = await _context.Orders.Where(o => o.User.Id == userId && !o.Confirmed).FirstOrDefaultAsync();
                var newOrderItem = _context.OrderItems.Add(
                    new OrderItem
                    {
                        Order = new Order
                        {
                            Confirmed = false,
                            //User = await _context.Users.FindAsync(userId),
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

        private bool OrderItemExists(string userEmail, int id)
        {
            return _context.OrderItems.Any(e => e.Order.User.Email == userEmail && e.ProductId == id && !e.Order.Confirmed);
        }
    }
}
