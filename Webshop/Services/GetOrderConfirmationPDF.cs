using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Webshop.Models;
using IronPdf;

namespace Webshop
{
    public class GetOrderConfirmationPDF
    {
        public static async Task<byte []> ViewToString(Controller controller, List<OrderItem> model)
        {
            controller.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, "Confirmation", false);

                if (viewResult.Success == false)
                {
                    throw new Exception("Could not find the page to convert into a PDF.");
                }

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );
                await viewResult.View.RenderAsync(viewContext);
                var result = writer.GetStringBuilder().ToString();
                HtmlToPdf renderer = new HtmlToPdf();
                
                return renderer.RenderHtmlAsPdf(result).BinaryData;
            }
        }
    }
}
