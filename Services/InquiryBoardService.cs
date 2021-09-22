using hospi_web_project.Models;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Services
{
    public class InquiryBoardService : IBoardService
    {
        private MySqlConnection conn { get; set; }

        public InquiryBoardService(DBService dbService)
        {
            conn = dbService.GetConnection();
        }

        public void DeleteBoard(int no)
        {
            try
            {
                conn.Open();

                string sql = "delete from inquiry where no=" + no;

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
            }
            finally
            {
                conn.Close();
            }
        }

        public BoardViewModel GetBoardDetail(int no)
        {
            try
            {
                conn.Open();

                string updateSQL = "update inquiry set Views=Views+1 where no =" + no;

                MySqlCommand updateCmd = new MySqlCommand(updateSQL, conn);

                if (updateCmd.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("Update Success!!");
                }
                else
                {
                    Console.WriteLine("Update Fail!!");
                }

                string selectSQL = "select * from inquiry where no=" + no;

                var model = new InquiryBoardViewModel();

                MySqlCommand selectCmd = new MySqlCommand(selectSQL, conn);
                MySqlDataReader rdr = selectCmd.ExecuteReader();
                while (rdr.Read())
                {
                    model.No = (int)rdr["No"];
                    model.Email = (string)rdr["Email"];
                    model.Title = (string)rdr["Title"];
                    model.Content = (string)rdr["Content"];
                    model.WriteDate = (string)rdr["WriteDate"];
                    model.Views = (int)rdr["Views"];
                    model.IsReply = (int)rdr["IsReply"];
                    model.Reply = (string)rdr["Reply"];
                    model.IsPrivate = (int)rdr["IsPrivate"];

                    object temp = rdr["File"];
                    if (temp.GetType() != typeof(DBNull)) model.File = (IFormFile)temp;
                    else model.File = null;
                }

                rdr.Close();

                return model;
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

        public List<BoardViewModel> GetBoardList()
        {
            try
            {
                conn.Open();
                string sql = "select * from inquiry order by No desc";

                var list = new List<BoardViewModel>();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var model = new InquiryBoardViewModel();

                    model.No = (int)rdr["No"];
                    model.Email = (string)rdr["Email"];
                    model.Title = (string)rdr["Title"];
                    model.WriteDate = (string)rdr["WriteDate"];
                    model.Views = (int)rdr["Views"];
                    model.IsPrivate = (int)rdr["IsPrivate"];
                    model.IsReply = (int)rdr["IsReply"];

                    list.Add(model);
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

        public List<BoardViewModel> SearchBoardList(string searchTxt)
        {
            try
            {
                conn.Open();
                string sql = "select * from inquiry where title like '%" + searchTxt + "%' order by No desc";

                var list = new List<BoardViewModel>();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var model = new InquiryBoardViewModel();

                    model.No = (int)rdr["No"];
                    model.Email = (string)rdr["Email"];
                    model.Title = (string)rdr["Title"];
                    model.WriteDate = (string)rdr["WriteDate"];
                    model.Views = (int)rdr["Views"];
                    model.IsPrivate = (int)rdr["IsPrivate"];
                    model.IsReply = (int)rdr["IsReply"];

                    list.Add(model);
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

        public void UpdateBoard(BoardViewModel model)
        {
            try
            {
                conn.Open();

                var inquiryVm = (InquiryBoardViewModel)model;

                IFormFile temp = inquiryVm.File;

                string sql;

                /*
                if (temp != null)
                {
                    sql = "update inquiry set Title='"
                    + inquiryVm.Title + "', Content='"
                    + inquiryVm.Content + "', File="
                    + inquiryVm.File
                    + " where No=" + inquiryVm.No;
                }
                else
                {
                    sql = "update inquiry set Title='"
                    + inquiryVm.Title + "', Content='"
                    + inquiryVm.Content + "', File=null"
                    + " where No=" + inquiryVm.No;
                }
                */

                sql = "update inquiry set Title='"
                    + inquiryVm.Title + "', Content='"
                    + inquiryVm.Content + "', File=null"
                    + " where No=" + inquiryVm.No;

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
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        public void WriteBoard(BoardViewModel model)
        {
            try
            {
                conn.Open();

                var inquiryVm = (InquiryBoardViewModel)model;

                string sql = "insert into inquiry values(null, '" 
                    + inquiryVm.Title + "', '"
                    + inquiryVm.Content + "', '"
                    + DateTime.Now.ToString("yyyy-MM-dd") +"', '"
                    + inquiryVm.Email +"', 0, null, 0, 0, null)";

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
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
