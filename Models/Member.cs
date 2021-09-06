﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Models
{
    public class Member
    {
        private DBService context;

        public string email { get; set; }

        public string password { get; set; }

        public string name { get; set; }

        public string birth { get; set; }

        public int sex { get; set; }

        public string phone { get; set; }
    }
}
