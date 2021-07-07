using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Controllers
{
    public class SupportController : Controller
    {
        public IActionResult ProductSupport()
        {
            return View();
        }

        public IActionResult PurchaseInformation()
        {
            return View();
        }

        public IActionResult RemoteAssistance()
        {
            return View();
        }

        public IActionResult Notice()
        {
            return View();
        }
    }
}
