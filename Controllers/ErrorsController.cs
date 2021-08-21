using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult _404()
        {
            return View();
        }
    }
}
