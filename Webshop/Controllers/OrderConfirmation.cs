using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Webshop.Models;
using System.Web;
using System.Security.Claims;

namespace Webshop.Controllers
{
    public class OrderConfirmationController : Controller
    {
        private UserManager<User> UserMgr { get; }
        private Task<User> GetCurrentUserAsync() => UserMgr.GetUserAsync(HttpContext.User);


        private readonly IdentityAppContext _context;
        private readonly IConfiguration _config;
        private string recipient;
        private string subject;
        private string body;
        private Order order;
        private List<OrderItem> orderItems;
        private string cartItems;
        private byte[] dataStream;
        private string purchaseConfirmation;
        private string email;
        private double totalAmount;
        private string paymentOption;
        private string deliveryOption;
        private string currency;

        public OrderConfirmationController(IdentityAppContext context, IConfiguration config, UserManager<User> userMgr)
        {
            _context = context;
            _config = config;
            UserMgr = userMgr;
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult SelectPaymentAndDeliveryOption(string totalprice)
        {
            ViewBag.totalPrice = totalprice;
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("cartItems")))
                return RedirectToAction("index", "Products");

            return View();
        }

        public IActionResult LoginOrRegister(string totalPrice)
        {
            if (User.Identity.IsAuthenticated)
            {
               return RedirectToAction("SelectPaymentAndDeliveryOption", "OrderConfirmation", new { totalprice = totalPrice });
            }

            ViewBag.totalPrice = totalPrice; 
            return View(); 
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<ActionResult> Confirmation(string totalPrice, string paymentType, string deliveryTime, string email)
        {
            var sumTotalPrice = double.Parse(totalPrice);
            var currentUser = await GetCurrentUserAsync();
            var userOrders = await _context.Orders.Where(x => x.User.Id == currentUser.Id).ToListAsync();
            if (!HttpContext.User.IsInRole("KeyCustomer") && userOrders.Count() > 2)
                await UserMgr.AddToRoleAsync(currentUser, "KeyCustomer");
            switch (deliveryTime)
            {
                case "2-5 days":
                    sumTotalPrice += 60;
                    break;
                case "5-10 days":
                    sumTotalPrice += 40;
                    break;
                case "30 days":
                    sumTotalPrice += 20;
                    break;
            }
            if (paymentType == "Invoice")
                sumTotalPrice += 50;
            if (await UserMgr.IsInRoleAsync(currentUser, "KeyCustomer"))
            {
                sumTotalPrice *= 0.9;
                ViewBag.keyCustomer = "You recived a 10% discount since you have been a loyal customer";
            }
            order = new Order
            {
                User = await _context.Users.FindAsync(currentUser.Id),
                PaymentOption = paymentType,
                TotalAmount = sumTotalPrice,
                DeliveryOption = deliveryTime,
                Confirmed = true
            };
             
            orderItems = new List<OrderItem>();
            cartItems = HttpContext.Session.GetString("cartItems");

            foreach (var id in cartItems)
            {
                if (!orderItems.Where(p => p.Product.Id == id - '0').Any())
                {
                    orderItems.Add(
                    new OrderItem
                    {
                        Order = order,
                        Product = await _context.Products.FindAsync(id - '0'),
                        Quantity = cartItems.Count(p => p.ToString() == id.ToString())
                    });
                }
            }
            await _context.AddRangeAsync(orderItems);
            await _context.SaveChangesAsync();

            ReciveConfirmationViaEmail(orderItems, email);

            ViewBag.totalPrice = sumTotalPrice.ToString();
            ViewBag.paymentType = paymentType;
            ViewBag.delivery = deliveryTime;
            ViewBag.orderId = order.Id;
            ViewBag.HideCurrencyConversion = true;

            HttpContext.Session.SetString("cartItems", "");

            return View(orderItems); 
        }  

        public async Task<ActionResult> DownloadConfirmationPdf(int orderId)
        {
            try
            {
                dataStream = await GetOrderConfirmationPDF.ViewToString(this, await GetOrderItemsByOrder(orderId));
                return File(dataStream, "application/pdf", "OrderConfirmation.pdf");
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrder(int orderId)
        {
            var orderItems = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderId == orderId);
          
            if(orderId <= 0 || orderItems.Count() <= 0)
            {
                throw new Exception("Could not find your Order.");
            }
            else
            {
                return await orderItems.ToListAsync();
            }
        }

        public void ReciveConfirmationViaEmail(List<OrderItem> orderItemsList, string inputEmail)
        {       

            purchaseConfirmation = "You successfully purchased: ";
            email = "";
            totalAmount = 0;
            paymentOption = "";
            deliveryOption = "";
            currency = "";

            foreach (var item in orderItemsList)
            {
                purchaseConfirmation += $"{item.Quantity} of {item.Product.Name}, ";
                totalAmount = item.Order.TotalAmount;
                paymentOption = item.Order.PaymentOption;
                deliveryOption = item.Order.DeliveryOption;
                currency = item.Order.User.Currency;
                email = inputEmail ?? item.Order.User.Email; 
            }

            purchaseConfirmation += $"for the amount of {CurrencyManager.CalcPrice((decimal)totalAmount, HttpContext.Session.GetString("currencyRate"))} {HttpContext.Session.GetString("currencyCode")} via { paymentOption}. Your delivery will arrive in { deliveryOption} days";

            recipient = email;
            subject = "Order Confirmation";
            body = purchaseConfirmation;
            MailMessage mm = new MailMessage();
            mm.To.Add(recipient);
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
        }


    }
}

