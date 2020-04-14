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
            var webshopContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(u => !u.Order.Confirmed && u.Order.User.Id == 1); // Add filter on current User once we have a user login system

            ViewBag.ListOfOrderItems = webshopContext.ToListAsync(); 
            
            return View(await webshopContext.ToListAsync());
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

        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.OrderId == id);
        }
    }
}
