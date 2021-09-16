using hospi_web_project.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Check { get; set; }
    }
}
