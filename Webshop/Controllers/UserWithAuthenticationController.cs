using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class UserWithAuthenticationController : Controller
    {
        
        private UserManager<User> UserMgr { get; }

        private SignInManager<User> SignInMgr { get; }

        private IdentityAppContext _context { get; set; }



        public UserWithAuthenticationController(UserManager<User> userManager,
           SignInManager<User> signInManager, IdentityAppContext identityAppContext)
        {
            UserMgr = userManager;
            SignInMgr = signInManager;
            _context = identityAppContext; 

        }


        public async Task<IActionResult> Logout()
        {
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Products"); 
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User model, string totalPrice)
        {
            var result = await SignInMgr.PasswordSignInAsync(model.Email, model.Password, false, false);


            if (result.Succeeded && totalPrice != null)
            {
                return RedirectToAction("SelectPaymentAndDeliveryOption", "OrderConfirmation", new { totalPrice = totalPrice, userEmail = model.Email, currency = model.Currency});
            }
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Products");
            }

        
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");


            return View(model);
        }


        public IActionResult Register()
        {
            return View(); 
        }


        [HttpPost]
        public async Task<IActionResult> Register(User model, string totalPrice)
        {
            var user = new User {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                City = model.City,
                StreetAdress = model.StreetAdress,
                PostNumber = model.PostNumber,
                CreatedAt = DateTime.Now,
                PhoneNumber = model.PhoneNumber,
                Currency = model.Currency
            };
            var result = await UserMgr.CreateAsync(user, model.Password);

            if (result.Succeeded && totalPrice != null)
            {
                await SignInMgr.SignInAsync(user, isPersistent: false);

                return RedirectToAction("SelectPaymentAndDeliveryOption", "OrderConfirmation", new { totalPrice = totalPrice, userEmail = model.Email, currency = model.Currency });
            }
            if (result.Succeeded)
            {
                await SignInMgr.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Products");

            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}