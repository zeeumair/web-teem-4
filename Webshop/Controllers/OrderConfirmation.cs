using System;
using System.Collections.Generic;
using System.Linq;
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
        private Order newOrder;

        public OrderConfirmationController(WebshopContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IActionResult SelectPaymentAndDeliveryOption(string productId, string orderId, string quantity, string orderItemId, string totalprice)
        {
          

            List<string> prodIdList = new List<string>(productId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            List<string> quantityList = new List<string>(quantity.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

            //decimal totalPrice = 0; 

            //foreach (var id in prodIdList)
            //{
            //    var context = _context.Products.Find(Int32.Parse(id));

            //    for (int i = 0; i < quantityList.Count(); i++)
            //    {
            //        totalPrice = totalPrice + context.Price * Convert.ToDecimal(quantityList[i]);

            //    }

            //}

            ViewBag.totalPrice = totalprice;
            ViewBag.productId = productId;
            ViewBag.orderId = orderId;
            ViewBag.orderItemId = orderItemId;
            ViewBag.quantity = quantity; 

            return View();
        }

    

        public async Task<ActionResult> Confirmation(string paymentType, string deliveryTime, double totalPrice, string productId, string orderId, string orderItemId, string quantity)
        {
            List<Product> products = new List<Product>();
            List<string> orderIdList = new List<string>(orderId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            List<string> orderItemIdList = new List<string>(orderItemId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            List<string> QuantityIdList = new List<string>(quantity.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

            foreach (var id in orderIdList)
            {
                var orderItemContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderId == Int32.Parse(id));

                foreach (var item in await orderItemContext.ToListAsync())
                {
                    products.Add(item.Product);
                    item.Order.Confirmed = true;
                    ViewBag.orderConfirmationNumber = item.OrderId;

                }
                await _context.SaveChangesAsync();
            }


         





            ViewBag.delivery = deliveryTime;
            ViewBag.paymentType = paymentType; 
            ViewBag.totalPrice = totalPrice;
            ViewBag.productId = productId;
            ViewBag.orderId = orderId;
            ViewBag.quantity = QuantityIdList;
            ViewBag.orderItemId = orderItemId;

            


            return View(products);
        }  

        public async Task<ActionResult> DownloadConfirmationPdf(string paymentType, string deliveryTime, double totalPrice, string productId, string orderId, string orderItemId, string quantity)
        {

            List<Product> products = new List<Product>();
            List<string> orderIdList = new List<string>(orderId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            List<string> orderItemIdList = new List<string>(orderItemId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            List<string> QuantityIdList = new List<string>(quantity.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

            foreach (var id in orderIdList)
            {
                var orderItemContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.OrderId == Int32.Parse(id));

                foreach (var item in await orderItemContext.ToListAsync())
                {
                    if(item.Product != null)
                        products.Add(item.Product);
                }
            }

            var pdfStream = await GetOrderConfirmationPDF.ViewToString(this, products, _config.GetValue<string>(WebHostDefaults.ContentRootKey));
            pdfStream.Position = 0;
            var result = new FileStreamResult(pdfStream, "application/pdf");
            result.FileDownloadName = "OrderConfirmation.pdf";

            return result;
        } 
    }

}

