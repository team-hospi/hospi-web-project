using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Models
{
    public class PaymentViewModel
    {
        public string OrderCode { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }
        public string ProductKey { get; set; }
        public string RegDate { get; set; }
    }
}
