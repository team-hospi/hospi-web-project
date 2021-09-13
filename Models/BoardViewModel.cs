using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Models
{
    public class BoardViewModel
    {
        public int No { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string WriteDate { get; set; }
        public int Views { get; set; }
    }
}
