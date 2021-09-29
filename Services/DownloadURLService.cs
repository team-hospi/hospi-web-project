using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Services
{
    public class DownloadURLService
    {
        private MySqlConnection conn { get; set; }

        public DownloadURLService(DBService dbService)
        {
            conn = dbService.GetConnection();
        }

        public string GetDownloadURL(string FileName)
        {
            try
            {
                conn.Open();

                string sql = "SELECT url FROM downloadurl where filename='" + FileName + "'";

                string url = null;

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    url = (string)rdr["url"];
                }
                rdr.Close();
                return url;
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
    }
}
