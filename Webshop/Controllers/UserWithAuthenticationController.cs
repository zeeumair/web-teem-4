using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class UserWithAuthenticationController : Controller
    {
        
        private UserManager<User> UserMgr { get; }

        private SignInManager<User> SignInMgr { get; }

        private readonly IdentityAppContext _context;
        private User newUser;

        public UserWithAuthenticationController(UserManager<User> userManager,
           SignInManager<User> signInManager, IdentityAppContext context)
        {
            UserMgr = userManager;
            SignInMgr = signInManager;
            _context = context;

        }


        public async Task<IActionResult> Logout()
        {
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Products"); 
        }

        public async Task<IActionResult> Login( string email, string password)
        {
           var test =  _context.Users.Where(e => e.UserName == email);

           var count = test.Count();


            var result = await SignInMgr.PasswordSignInAsync(email, password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Products");
            }
            else
            {
                ViewBag.Result = "result is: " + result.ToString(); 
            }

            return View(); 
        }

        public IActionResult Register()
        {
            return View(); 
        }


        public async Task<IActionResult> AddNewUser(string firstName, string lastName, string password, string streetAdress, string postNumber, string city, string country, string email, string phoneNumber)
        {

            //ArgumentException: The key value at position 0 of the call to 'DbSet<User>.Find' was of type 'string', which does not match the property type of 'int'.


            var user = _context.Users.Find(email);

                var x = user.Email; 


                if (x != email)
                {

                    newUser = new User
                    {                 
                        FirstName = firstName,
                        LastName = lastName,
                        Password = password,
                        StreetAdress = streetAdress,
                        PostNumber = postNumber,
                        City = city,
                        Country = country,
                        Email = email,
                        PhoneNumber = phoneNumber
                    };



                    //IdentityResult result = await UserMgr.CreateAsync(user, user.Password);
                     _context.Add(user);
                    await _context.SaveChangesAsync();

                    IdentityResult result = await UserMgr.CreateAsync(newUser, newUser.Password); 

                    user.UserName = newUser.Email;

                    await _context.SaveChangesAsync();


                    return RedirectToAction("Login", "UserWithAuthentication", new { email = newUser.Email, password = newUser.Password });


                }
            
         

            ViewBag.Message = "Email is allready registered";


            return RedirectToAction("Register", "UserWithAuthentication");  
        }


    }
}