using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Service
{
    public class PaymentService
    {
        private MySqlConnection conn { get; set; }

        public PaymentService(DBService dbService)
        {
            conn = dbService.GetConnection();
        }
    }
}
