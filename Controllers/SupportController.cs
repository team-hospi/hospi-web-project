using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Controllers
{
    public class SupportController : Controller
    {
        public IActionResult ProductAuthGuide()
        {
            return View();
        }

        public IActionResult ProductSupport()
        {
            return View();
        }

        public IActionResult Notice()
        {
            return View();
        }

        public IActionResult Inquiry()
        {
            return View();
        }

        public IActionResult InquiryCreate()
        {
            return View();
        }

        public IActionResult InquiryDelete()
        {
            return View();
        }

        public IActionResult InquiryDetails()
        {
            return View();
        }

        public IActionResult InquiryEdit()
        {
            return View();
        }
    }
}
