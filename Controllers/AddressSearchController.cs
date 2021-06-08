using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Controllers
{
    public class AddressSearchController : Controller
    {
        public IActionResult Mobile()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }
    }
}
