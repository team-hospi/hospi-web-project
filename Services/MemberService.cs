using hospi_web_project.Models;
using hospi_web_project.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace hospi_web_project.Services
{
    public class MemberService
    {
        private MySqlConnection conn { get; set; }

        public MemberService(DBService dbService)
        {
            conn = dbService.GetConnection();
        }
        
        public MemberViewModel login(LoginViewModel model)
        {
            MemberViewModel member = new MemberViewModel();
            try
            {
                conn.Open();

                string encryptPW = EncryptionTool.SHA256Hash(model.Password, model.Email);

                string sql = "select * from member where email=\"" + model.Email + "\";";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    if (rdr["email"].Equals(model.Email))
                    {
                        if (rdr["password"].Equals(encryptPW))
                        {
                            member.name = (string)rdr["name"];
                            member.birth = (string)rdr["birth"];
                            member.sex = (string)rdr["sex"];
                            member.phone = (string)rdr["phone"];
                            return member;
                        }
                        else
                        {
                            member = null;
                            return member;
                        }
                    }
                    else
                    {
                        member = null;
                        return member;
                    }
                }
                rdr.Close();
                return member = null;
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

        public MemberViewModel GetMemberInfo(string email)
        {
            try
            {
                conn.Open();
                string sql = "select * from member where email=\"" + email + "\";";

                MemberViewModel member = new();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    member.email = (string)rdr["email"];
                    member.password = (string)rdr["password"];
                    member.name = (string)rdr["name"];
                    member.birth = (string)rdr["birth"];
                    member.sex = (string)rdr["sex"];
                    member.phone = (string)rdr["phone"];
                }
                rdr.Close();

                return member;
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

        public bool CheckPassword(string email, string pw)
        {
            try
            {
                conn.Open();
                string sql = "select Password from member where email=\"" + email + "\";";

                string pw2 = null;

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    pw2 = (string)rdr["password"];
                }
                rdr.Close();

                if(EncryptionTool.SHA256Hash(pw, email) != pw2)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public void ChangePassword(string email, string pw)
        {
            try
            {
                conn.Open();

                string sql;

                sql = "update member set " +
                    "Password='" + EncryptionTool.SHA256Hash(pw, email) + "' " +
                    "where Email='" + email + "'";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("Update Success!!");
                }
                else
                {
                    Console.WriteLine("Update Fail!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateBirth(string email, string birth)
        {
            try
            {
                conn.Open();

                string sql;

                sql = "update member set " +
                    "Birth='" + birth + "' " +
                    "where Email='" + email + "'";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("Update Success!!");
                }
                else
                {
                    Console.WriteLine("Update Fail!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdatePhone(string email, string phone)
        {
            try
            {
                conn.Open();

                string sql;

                sql = "update member set " +
                    "Phone='" + phone + "' " +
                    "where Email='" + email + "'";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("Update Success!!");
                }
                else
                {
                    Console.WriteLine("Update Fail!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Withdrawal(string email)
        {
            try
            {
                conn.Open();

                string sql;

                sql = "delete from member where Email='" + email + "'; "
                    + "delete from management where Email='" + email + "'; ";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("Delete Success!!");
                }
                else
                {
                    Console.WriteLine("Delete Fail!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Debug.WriteLine("예외 발생: " + e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
