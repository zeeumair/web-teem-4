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
    public class ProductsController : Controller
    {
        private readonly IdentityAppContext _context;

        public ProductsController(IdentityAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string search = null)
        {
            var products = from c in _context.Products
                           select c;

            if(!String.IsNullOrEmpty(search))
            {
                products = products.Where(x => x.Name.Contains(search) || x.Description.Contains(search) || x.Category.Contains(search));
            }

            await products.ToListAsync();

            Queue<double> averageStars = new Queue<double>();
            foreach (var product in products)
            {
                var reviews = await _context.Reviews.Where(x => x.ProductId == product.Id).ToListAsync();
                if (reviews.Any())
                {
                   averageStars.Enqueue(reviews.Average(x => x.Stars));
                }
                else
                {
                    averageStars.Enqueue(-1);
                }
                
            }
            ViewBag.AverageStars = averageStars;
            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var reviews = await _context.Reviews.Where(x => x.ProductId == id).ToListAsync();
            ViewBag.Reviews = reviews;
            return View(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
