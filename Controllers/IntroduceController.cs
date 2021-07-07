using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Controllers
{
    public class IntroduceController : Controller
    {
        public IActionResult CompanyIntroduction()
        {
            return View();
        }

        public IActionResult ProductIntroduction()
        {
            return View();
        }
    }
}
