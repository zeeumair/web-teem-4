using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
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

        public async Task<IActionResult> AddReviewToProduct(int productId, int stars, string description)
        {
            var currentUser = await GetCurrentUserAsync();
            await _context.Reviews.AddAsync(
                new Review
                {
                    ProductId = productId,
                    UserId = currentUser.Id,
                    Stars = stars,
                    Description = description
                }
            );
            await _context.SaveChangesAsync();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> OrderItemsHistory(int id)
        {
            var currentUser = await GetCurrentUserAsync();
            ViewBag.OrderId = id;
            var reviews = new List<Review>();
            var orderItems = await _context.OrderItems.Where(x => x.Order.Id == id).Include("Product").ToListAsync();
            
            foreach (var item in orderItems)
            {
                var review = await _context.Reviews.Where(x => x.ProductId == item.Product.Id && x.User.Id == currentUser.Id).FirstOrDefaultAsync();
                if (review != null)
                {
                    reviews.Add(review);
                }
                else
                {
                    reviews.Add(null);
                }
            }
            ViewBag.Reviews = reviews;
            return View(orderItems);
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(int productId, int stars, string description)
        {
            var currentUser = await GetCurrentUserAsync();
            if (false) // Här ska vi kolla om den finns, isf ska vi bara uppdatera.
            {
            }
            else
            {
                await _context.Reviews.AddAsync(new Review{ UserId = currentUser.Id, ProductId = productId, Stars = stars, Description = description }); 
            }

            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
