using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly WebshopContext _context;

        public OrdersController(WebshopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OrderHistory(int? id)
        {
            var userId = 0;
            if (id != null)
            {
                userId = _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync().Id;
                return View(await _context.Orders.Where(x => x.User.Id == userId).ToListAsync());
            }
            else
            {
                return View(await _context.Orders.ToListAsync());
            }
        }

        public async Task<IActionResult> OrderItemsHistory(int id)
        {
            ViewBag.OrderId = id;
            return View(await _context.OrderItems.Where(x => x.Order.Id == id).ToListAsync());
        }
    }
}
