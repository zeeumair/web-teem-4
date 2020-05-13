using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

            return View(await _context.Products.Where(p => HttpContext.Session.GetString("cartItems").Contains(p.Id.ToString())).ToListAsync());

        }

        public async Task<IActionResult> AddProductToCart(int id, string userEmail)
        {
            await Task.Run(() =>
            {
                HttpContext.Session.GetString("cartItems");
                string cartItems = HttpContext.Session.GetString("cartItems");
                if (String.IsNullOrEmpty(cartItems))
                    HttpContext.Session.SetString("cartItems", id.ToString());
                else
                    HttpContext.Session.SetString("cartItems", cartItems + id.ToString());
            });

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> DecrementProductQuantity(int? id)
        {
            await Task.Run(() =>
            {
                var currentItems = HttpContext.Session.GetString("cartItems");
                var updatedCart = currentItems.Remove(currentItems.IndexOf(id.ToString()), 1);
                HttpContext.Session.SetString("cartItems", updatedCart);
            });
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> IncrementProductQuantity(int? id)
        {
            await Task.Run(() =>
            {
                var currentItems = HttpContext.Session.GetString("cartItems");
                string updatedCart = currentItems + id.ToString();
                HttpContext.Session.SetString("cartItems", updatedCart);
            });
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemExists(string userEmail, int id)
        {
            return _context.OrderItems.Any(e => e.Order.User.Email == userEmail && e.ProductId == id && !e.Order.Confirmed);
        }

        private bool CartItemExists(string cartItems, int id)
        {
            return cartItems.Contains(id.ToString());
        }
    }
}
