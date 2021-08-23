using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Controllers
{
    public class InquiryController : Controller
    {
        public IActionResult GeneralInquiry()
        {
            return View();
        }

        public IActionResult ProductInquiry()
        {
            return View();
        }
        public IActionResult InquirySuccess()
        {
            return View();
        }
    }
}
