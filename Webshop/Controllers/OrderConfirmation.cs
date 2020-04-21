using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class OrderConfirmationController : Controller
    {

        private readonly WebshopContext _context;
        private readonly IConfiguration _config;

        public OrderConfirmationController(WebshopContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IActionResult SelectPaymentAndDeliveryOption(string productId, int orderId, string quantity, string orderItemId, string totalprice, List<OrderItem> orderItemsList)
        {

            var things = TempData["Things"] as IEnumerable<string>;

            TempData["Things2"] = things;

            var x = orderItemsList.Count();  

            ViewBag.orderItemsList = orderItemsList;

            ViewBag.totalPrice = totalprice;
            ViewBag.productId = productId;
            ViewBag.orderId = orderId.ToString();
            ViewBag.orderItemId = orderItemId;
            ViewBag.quantity = quantity;



            return View();
        }

        public async Task<ActionResult> Confirmation(int orderId, string totalPrice, string paymentType, string deliveryTime, string productId)
        {
           

            var things = TempData["Things2"] as IEnumerable<string>;

            IQueryable<OrderItem> orderItemIqueryableForView;
            orderItemIqueryableForView = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where( o => o.OrderId == orderId);

       

            IQueryable<OrderItem> orderItemIqueryable;
            List<OrderItem> orderItemsList = new List<OrderItem>();

            orderItemIqueryable = _context.OrderItems.Include(o => o.Order).Include(o => o.Product);
            


                foreach (var orderItemss in orderItemIqueryable)
                {
                    if(things.Contains(orderItemss.OrderId.ToString()))
                    {
                        orderItemss.Order.PaymentOption = paymentType;
                        orderItemss.Order.DeliveryOption = deliveryTime;
                        orderItemss.Order.TotalAmount = Convert.ToDouble(totalPrice);
                        orderItemss.Order.Confirmed = true;
                        
                        orderItemsList.Add(orderItemss);

                    }
                }

            await _context.SaveChangesAsync();


            //var orderItem = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderId == );



            //foreach (var orderItemId in things)
            //{
            //    orderitems = _context.OrderItems.Find(orderItemId);
            //    orderitems.Order.PaymentOption = paymentType;
            //    orderitems.Order.DeliveryOption = deliveryTime;
            //    orderitems.Order.TotalAmount = Convert.ToDouble(totalPrice);
            //    orderitems.Order.Confirmed = true;

            //    orderItemsList.Add(orderitems);

            //    await _context.SaveChangesAsync();
            //}

            ReciveConfirmationViaEmail(orderItemsList);

            ViewBag.totalPrice = totalPrice;
            ViewBag.paymentType = paymentType;
            ViewBag.delivery = deliveryTime;
            ViewBag.orderId = orderId.ToString();
            //try
            //{
            //    var orderItems = await GetOrderItemsByOrder(orderId);
            //    orderItems.ForEach(o => { o.Order.Confirmed = true; });

            //    await _context.SaveChangesAsync();

            //    return View(orderItems);
            //}
            //catch(Exception e)
            //{

            //    return NotFound(e.Message);
            //}
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

            //Edit when user is availible
            var email = "omgzshoezz@gmail.com";
            double totalAmount = 0;
            var paymentOption = "";
            var deliveryOption = "";

            foreach (var item in orderItemsList)
            {
                purchaseConfirmation = purchaseConfirmation + $"{item.Quantity} of {item.Product.Name}, ";
                totalAmount = item.Order.TotalAmount;
                paymentOption = item.Order.PaymentOption;
                deliveryOption = item.Order.DeliveryOption;
            }

            purchaseConfirmation = purchaseConfirmation + $"for the amount of ${totalAmount}) via { paymentOption}. Your delivery will arrive in { deliveryOption} days";

          var x =  purchaseConfirmation;

            //var orderItem = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderId == Int32.Parse(orderId));
            //var quantity = 0;
            ////Edit when user is availible
            //var email = "petter.fagerlund@gmail.com";
            //double totalAmount = 100;
            //var paymentOption = "swish";
            //var deliveryOption = "dhl";
            //var productName = "nike air"; 

            //foreach (var item in orderItem.ToList())
            //{
            //    quantity = item.Quantity;
            //    //product is not present ???
            //    productName = item.Product.Name;

            //    //Edit when user is availible
            //    // email = item.Order.User.Email;

            //    //Order items below is not present???

            //    totalAmount = item.Order.TotalAmount;
            //    paymentOption = item.Order.PaymentOption;
            //    deliveryOption = item.Order.DeliveryOption;
            //}



            string to = email;
            string subject = "Order Confirmation";
            string body = purchaseConfirmation;
            MailMessage mm = new MailMessage();
            mm.To.Add(to);
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

