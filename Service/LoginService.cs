using hospi_web_project.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Service
{
    public class LoginService
    {
        private MySqlConnection conn { get; set; }

        public LoginService(DBService dbService)
        {
            conn = dbService.GetConnection();
        }

        public MemberViewModel login(LoginViewModel model)
        {
            MemberViewModel member = new MemberViewModel();
            try
            {
                conn.Open();
                string sql = "select * from member where email=\"" + model.Email + "\";";

                //ExecuteReader를 이용하여
                //연결 모드로 데이타 가져오기
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr["email"].Equals(model.Email))
                    {
                        if (rdr["password"].Equals(model.Password))
                        {
                            member.name = (string)rdr["name"];
                            member.birth = (string)rdr["birth"];
                            member.sex = (string)rdr["sex"];
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
