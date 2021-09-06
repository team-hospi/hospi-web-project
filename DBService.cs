using hospi_web_project.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project
{
    public class DBService
    {
        private static string connectionString = "Server=katep.iptime.org;Database=hospi;Uid=hospi;Pwd=@hospi0524!23;";
        private MySqlConnection conn = new MySqlConnection(connectionString);

        public Member login(LoginViewModel model)
        {
            Member member = new Member();
            try
            {
                conn.Open();
                string sql = "select * from member where email=\""+ model.UserEmail + "\";";

                //ExecuteReader를 이용하여
                //연결 모드로 데이타 가져오기
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr["email"].Equals(model.UserEmail))
                    {
                        if (rdr["password"].Equals(model.UserPassword))
                        {
                            member.name = (string)rdr["name"];
                            member.birth = (string)rdr["birth"];
                            member.sex = (int)rdr["sex"];
                            member.phone = (string)rdr["phone"];
                        }
                        else
                        {
                            member = null;
                        }
                    }
                    else
                    {
                        member = null;
                    }
                }
                rdr.Close();
                return member;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return member;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
