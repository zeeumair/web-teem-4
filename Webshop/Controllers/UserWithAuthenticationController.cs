using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class UserWithAuthenticationController : Controller
    {
        private User userToUpdate;
        private User user;
        private Microsoft.AspNetCore.Identity.SignInResult result;
        private User currentUser;
        private IQueryable<OrderItem> orderHistory;
        private string token;
        private string passwordResetLink;
        private string to;
        private string subject;
        private string body;

        private UserManager<User> UserMgr { get; }

        private SignInManager<User> SignInMgr { get; }

        private IdentityAppContext Context { get; set; }
        public Task<bool> IsKeyCustomer { get; private set; }
        public double DiscountedPrice { get; private set; }

        private Task<User> GetCurrentUserAsync() => UserMgr.GetUserAsync(HttpContext.User);



        public UserWithAuthenticationController(UserManager<User> userManager,
           SignInManager<User> signInManager, IdentityAppContext identityAppContext)
        {
            UserMgr = userManager;
            SignInMgr = signInManager;
            Context = identityAppContext; 

        }

        public async Task<IActionResult> Edit()
        {
             user = await GetCurrentUserAsync();

            return View(user); 
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            userToUpdate = await UserMgr.FindByEmailAsync(user.Email);

            userToUpdate.Email = user.Email;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            userToUpdate.StreetAdress = user.StreetAdress;
            userToUpdate.City = user.City;
            userToUpdate.PostNumber = user.PostNumber;
            userToUpdate.LastName = user.LastName;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.Country = user.Country;
            userToUpdate.Currency = user.Currency;
            userToUpdate.UserName = user.Email; 

            await UserMgr.UpdateAsync(userToUpdate);


            return View(userToUpdate);
        }

        public async Task<IActionResult> Logout()
        {
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Products"); 
        }

        public async Task<User> ChangeToKeyCustomer(User currentUser)
        {

             orderHistory = Context.OrderItems.Include(o => o.Order).Include(ou => ou.Order.User).Where(ou => ou.Order.User == currentUser && ou.Order.Confirmed == true);

            if (orderHistory.Count() > 2)
            {
                await UserMgr.AddToRoleAsync(currentUser, "KeyCustomer");
            }

            return currentUser; 
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User model, string totalPrice)
        {
            result = await SignInMgr.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded && totalPrice != null)
            {
               currentUser = await UserMgr.FindByEmailAsync(model.Email);

               await ChangeToKeyCustomer(currentUser); 

               IsKeyCustomer = UserMgr.IsInRoleAsync(currentUser, "KeyCustomer");
               DiscountedPrice = Convert.ToDouble(totalPrice);

                if (IsKeyCustomer.Result == true)
                {
                    DiscountedPrice *= 0.9;
                    
                    return RedirectToAction("SelectPaymentAndDeliveryOption", "OrderConfirmation", new { totalPrice = DiscountedPrice.ToString(), keyCustomer = IsKeyCustomer.Result });
                }

                return RedirectToAction("SelectPaymentAndDeliveryOption", "OrderConfirmation", new { totalPrice, keyCustomer = IsKeyCustomer.Result });

            }
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Products");
            }

        
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");


            return View(model);
        }

        public IActionResult GoogleLogin()
        {
            var properties = SignInMgr.ConfigureExternalAuthenticationProperties("Google", Url.Action("GoogleLoginCallback", "UserWithAuthentication"));
            return new ChallengeResult("Google", properties);
        }

        public async Task<IActionResult> GoogleLoginCallback(string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Invalid Login Attempt: {remoteError}");
                return RedirectToAction("Login");
            }
            
            var info = await SignInMgr.GetExternalLoginInfoAsync();
            
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                return RedirectToAction("Login");
            }

            var googleEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
            
            if (String.IsNullOrEmpty(googleEmail))
            {
                ModelState.AddModelError(string.Empty, "Could not find an email associated with your google account. Make sure your Google account has a registered email address.");
                return RedirectToAction("Login");
            }

            var matchingUser = Context.Users.Where(u => u.Email == googleEmail).FirstOrDefault();

            if (matchingUser != null && !Context.UserLogins.Where(u => u.UserId == matchingUser.Id).Any())
            {
                ModelState.AddModelError(string.Empty, "An account with that email address already exists.");
                return RedirectToAction("Login");
            }

            var result = await SignInMgr.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            
            if (result.Succeeded)
                return RedirectToAction("Index", "Products");

            var user = new User
            {
                Email = googleEmail,
                UserName = googleEmail,
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                StreetAdress = info.Principal.FindFirstValue(ClaimTypes.StreetAddress),
                PostNumber = info.Principal.FindFirstValue(ClaimTypes.PostalCode),
                City = info.Principal.FindFirstValue(ClaimTypes.Locality),
                Country = info.Principal.FindFirstValue(ClaimTypes.Country),
                Currency = "SEK",
                PhoneNumber = info.Principal.FindFirstValue(ClaimTypes.MobilePhone),
                Password = null,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            await UserMgr.CreateAsync(user);
            await UserMgr.AddLoginAsync(user, info);
            await SignInMgr.SignInAsync(user, false);

            return RedirectToAction("Index", "Products");
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
                Currency = model.Currency,
                Country = model.Country
            };
            
            var result = await UserMgr.CreateAsync(user, model.Password);


            if (result.Succeeded && totalPrice != null)
            {
                await SignInMgr.SignInAsync(user, isPersistent: false);

                return RedirectToAction("SelectPaymentAndDeliveryOption", "OrderConfirmation", new { totalPrice });
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
          return View(); 
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserMgr.FindByEmailAsync(model.Email);

                if (user != null)
                {
                     token = await UserMgr.GeneratePasswordResetTokenAsync(user);
                     passwordResetLink = Url.Action("ResetPassword", "UserWithAuthentication",
                        new { token, email = model.Email}, Request.Scheme);

                    SendResetPasswordLink(passwordResetLink, model.Email);

                    return View("ForgotPasswordConfirmation");
                }
                return View("ForgotPasswordConfirmation");
            }

            return View(model);

        }

        public IActionResult SendResetPasswordLink(string resetPasswordLink, string email)
        {
            to = email;
            subject = "Reset Password";
            body = $"Please click the link to reset your password {resetPasswordLink}";
            MailMessage mm = new MailMessage();
            mm.To.Add(to);
            mm.Subject = subject;
            mm.Body = body;
            mm.From = new MailAddress("omgzshoezz@gmail.com", "OMGZ Shoes");
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("omgzshoezz@gmail.com", "OmgzOmgz123")
            };
            smtp.Send(mm);

            return View();
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View(); 
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password Reset Token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserMgr.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await UserMgr.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                return View("ResetPasswordConfirmation");
            }
            return View(model);
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

    }
}