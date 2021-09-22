using hospi_web_project.Models;
using hospi_web_project.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace hospi_web_project.Services
{
    public class SignUpService
    {
        private MySqlConnection conn { get; set; }

        public SignUpService(DBService dbService)
        {
            conn = dbService.GetConnection();
        }

        public void Signup(MemberViewModel member)
        {
            try
            {
                conn.Open();

                string encryptPW = EncryptionTool.SHA256Hash(member.password, member.email);

                string sql = "INSERT INTO Member(Email, Password, Name, Birth, Sex, Phone) VALUES('" + member.email + "', '" + encryptPW + "', '" + member.name + "', '" + member.birth + "', '" + member.sex + "', '" + member.phone.Substring(0, 3) + "-" + member.phone.Substring(3, 4) + "-" + member.phone.Substring(7, 4) + "')";

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
