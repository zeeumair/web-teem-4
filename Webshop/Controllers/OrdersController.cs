using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IdentityAppContext _context;
        private UserManager<User> UserMgr { get; }
        private Task<User> GetCurrentUserAsync() => UserMgr.GetUserAsync(HttpContext.User);
        public OrdersController(IdentityAppContext context, UserManager<User> userMgr)
        {
            _context = context;
            UserMgr = userMgr;

        }

        public async Task<IActionResult> OrderHistory()
        {
            var currentUser = await GetCurrentUserAsync();

            try
            {
                return View(await _context.Orders.Where(x => x.User.Id == currentUser.Id).ToListAsync());
            }
            catch
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> OrderItemsHistory(int id)
        {
            ViewBag.OrderId = id;
            return View(await _context.OrderItems.Where(x => x.Order.Id == id).Include("Product").ToListAsync());
        }
    }
}
