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

        // GET: ShoppingCart
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
               await Task.Run(() => Delete(currentItems));
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

        // GET: ShoppingCart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: ShoppingCart/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: ShoppingCart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,Quantity,Confirmed")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: ShoppingCart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", orderItem.ProductId);
            return View(orderItem);
        }

        // POST: ShoppingCart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ProductId,Quantity,Confirmed")] OrderItem orderItem)
        {
            if (id != orderItem.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: ShoppingCart/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    var orderItem = await _context.OrderItems
        //        .Include(o => o.Order)
        //        .Include(o => o.Product)
        //        .FirstOrDefaultAsync(m => m.OrderId == id);
        //    if (orderItem == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    return View(orderItem);
        //}

        // POST: ShoppingCart/Delete/5
        

        public async void Delete(OrderItem orderItem)
        {
            //if (id == null)
            //{
            //    Console.WriteLine("Params null");
            //    return NotFound();
            //}
            //
            //var orderItem = await _context.OrderItems.Where(o => o.OrderId == id && o.ProductId == idPart).FirstOrDefaultAsync();
            ////FindAsync(id);
            ////Where(id => id.OrderId == oId && id.ProductId == pId).FirstOrDefaultAsync();
            //if (orderItem == null)
            //{
            //    Console.WriteLine("orderItem == null");
            //}
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }

        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.OrderId == id);
        }
    }
}
