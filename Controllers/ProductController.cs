using hospi_web_project.Models;
using hospi_web_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Windows()
        {
            return View();
        }

        public IActionResult Android()
        {
            return View();
        }

        public IActionResult Purchase()
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            ProductService context = new(dbService);

            return View(context.GetProductList());
        }

        [Authorize]
        public IActionResult Payment()
        {
            return View();
        }

        public IActionResult PaymentSuccess()
        {
            return View();
        }

        public IActionResult PaymentProcess(PaymentViewModel model)
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            PaymentService context = new(dbService);

            context.PaymentProcess(model);

            return RedirectToAction("PaymentSuccess", "Product");
        }
    }
}
