﻿using hospi_web_project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public MemberViewModel login(MemberViewModel member)
        {
            try
            {
                conn.Open();
                string sql = "select * from member where email=\"" + member.email + "\";";

                //ExecuteReader를 이용하여
                //연결 모드로 데이타 가져오기
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    if (rdr["email"].Equals(member.email))
                    {
                        if (rdr["password"].Equals(member.password))
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
    }
}
