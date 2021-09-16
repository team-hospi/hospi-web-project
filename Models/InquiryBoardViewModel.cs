using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Models
{
    public class InquiryBoardViewModel : BoardViewModel
    {
        public IFormFile File { get; set; }
        public int Private { get; set; }
    }
}
