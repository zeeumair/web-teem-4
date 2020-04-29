using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            return View(await _context.Products.Where(p => HttpContext.Session.GetString("cartItems").Contains(p.Id.ToString())).ToListAsync());
        }

        public async Task<IActionResult> AddProductToCart(int id)
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

        private bool CartItemExists(string cartItems, int id)
        {
            return cartItems.Contains(id.ToString());
        }
    }
}
