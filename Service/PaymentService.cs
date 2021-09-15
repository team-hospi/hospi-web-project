using hospi_web_project.Models;
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

        public List<PaymentViewModel> GetPaymentList(string email)
        {
            try
            {
                conn.Open();
                string sql = "select payment.OrderCode, payment.Email, product.ProductName, payment.ProductKey, payment.RegDate\n"
                    + "from payment\n"
                    + "left join product on payment.ProductCode = product.ProductCode\n"
                    + "where payment.Email='" + email + "'";

                List<PaymentViewModel> list = new();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    PaymentViewModel vm = new();
                    vm.OrderCode = (string)rdr["OrderCode"];
                    vm.Email = (string)rdr["Email"];
                    vm.ProductName = (string)rdr["ProductName"];
                    vm.ProductKey = (string)rdr["ProductKey"];
                    vm.RegDate = (string)rdr["RegDate"];

                    list.Add(vm);
                }
                rdr.Close();

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public void PaymentProcess(PaymentViewModel payment)
        {
            try
            {
                conn.Open();

                string sql = "INSERT INTO Payment(OrderCode, Email, ProductCode, ProductKey, RegDate) VALUES('" + payment.OrderCode + "', '" + payment.Email + "', '" + Convert.ToInt32(payment.ProductName) + "', '" + payment.ProductKey + "', '" + payment.RegDate + "')";

                //ExecuteReader를 이용하여
                //연결 모드로 데이타 가져오기
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("Insert Success!!");
                }
                else
                {
                    Console.WriteLine("Insert Fail!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
