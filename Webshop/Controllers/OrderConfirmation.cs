using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class OrderConfirmationController : Controller
    {

        private readonly IdentityAppContext _context;
        private readonly IConfiguration _config;
        private string recipient;
        private string subject;
        private string body;
        private string orderId;
        private IEnumerable<string> orderedItems;
        private IEnumerable<string> orderItems;

        public OrderConfirmationController(IdentityAppContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IActionResult SelectPaymentAndDeliveryOption(string totalprice, string userEmail)
        {
            SetUserToOrder(userEmail); 

            ViewBag.totalPrice = totalprice;
            ViewBag.orderId = orderId.ToString();

            orderItems = TempData["OrderItems"] as IEnumerable<string>;
            TempData["OrderedItems"] = orderItems;

            return View();
        }

        public void SetUserToOrder(string userEmail)
        {
            orderItems = TempData["OrderItems"] as IEnumerable<string>;

            var orderItemIqueryable = _context.OrderItems.Include(o => o.Order).Include(o => o.Product);
            var user = _context.Users.Where(u => u.Email == userEmail).Single();

            foreach (var item in orderItemIqueryable)
            {
                if (orderItems.Contains(item.OrderId.ToString()))
                {
                    item.Order.User = user;
                    _context.SaveChangesAsync();
                }
            }
        }

        public IActionResult LoginOrRegister(string totalPrice)
        {
            ViewBag.totalPrice = totalPrice; 
            return View(); 
        }

        public async Task<ActionResult> Confirmation(string totalPrice, string paymentType, string deliveryTime)
        {

            orderedItems = TempData["OrderedItems"] as IEnumerable<string>;
            orderId = "";

            IQueryable<OrderItem> orderItemIqueryable;
            orderItemIqueryable = _context.OrderItems.Include(o => o.Order).Include(o => o.Product);

            List<OrderItem> orderItemsList = new List<OrderItem>();


            foreach (var orderItemss in orderItemIqueryable)
                {
                    if(orderedItems.Contains(orderItemss.OrderId.ToString()))
                    {
                        orderItemss.Order.PaymentOption = paymentType;
                        orderItemss.Order.DeliveryOption = deliveryTime;
                        orderItemss.Order.TotalAmount = Convert.ToDouble(totalPrice);
                        orderItemss.Order.Confirmed = true;
                        
                        orderId = orderItemss.OrderId.ToString(); 
                        
                        orderItemsList.Add(orderItemss);

                    }
                }

            await _context.SaveChangesAsync();

            ReciveConfirmationViaEmail(orderItemsList);

            IQueryable<OrderItem> orderItemIqueryableForView;
            orderItemIqueryableForView = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderId == Int32.Parse(orderId));


            ViewBag.totalPrice = totalPrice;
            ViewBag.paymentType = paymentType;
            ViewBag.delivery = deliveryTime;
            ViewBag.orderId = orderId;

            return View(orderItemIqueryableForView); 
        }  

        public async Task<ActionResult> DownloadConfirmationPdf(int orderId)
        {
            try
            {
                var dataStream = await GetOrderConfirmationPDF.ViewToString(this, await GetOrderItemsByOrder(orderId, true));
                return File(dataStream, "application/pdf", "OrderConfirmation.pdf");
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrder(int orderId, bool includeConfirmed = false)
        {
            var orderItems = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderId == orderId && o.Order.Confirmed == includeConfirmed);
            if(orderId <= 0 || orderItems.Count() <= 0)
            {
                throw new Exception("Could not find your Order.");
            }
            else
            {
                return await orderItems.ToListAsync();
            }
        }

        public void ReciveConfirmationViaEmail(List<OrderItem> orderItemsList)
        {       

            string purchaseConfirmation = "You successfully purchased: ";

            var email = "";
            double totalAmount = 0;
            var paymentOption = "";
            var deliveryOption = "";

            foreach (var item in orderItemsList)
            {
                purchaseConfirmation = purchaseConfirmation + $"{item.Quantity} of {item.Product.Name}, ";
                totalAmount = item.Order.TotalAmount;
                paymentOption = item.Order.PaymentOption;
                deliveryOption = item.Order.DeliveryOption;
                email = item.Order.User.Email; 
            }

            purchaseConfirmation = purchaseConfirmation + $"for the amount of ${totalAmount}) via { paymentOption}. Your delivery will arrive in { deliveryOption} days";
            recipient = email;
            subject = "Order Confirmation";
            body = purchaseConfirmation;
            MailMessage mm = new MailMessage();
            mm.To.Add(recipient);
            mm.Subject = subject;
            mm.Body = body;
            mm.From = new MailAddress("omgzshoezz@gmail.com", "OMGZ Shoes");
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("omgzshoezz@gmail.com", "OmgzOmgz123");
            smtp.Send(mm);
        }
    }
}

