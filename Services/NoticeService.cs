using hospi_web_project.Models;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace hospi_web_project.Services
{
    public class NoticeService : IBoardService
    {
        private MySqlConnection conn { get; set; }

        public NoticeService(DBService dbService)
        {
            conn = dbService.GetConnection();
        }

        public void DeleteBoard(int no)
        {
            try
            {
                conn.Open();

                string sql = "delete from notice where no=" + no;

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

                string updateSQL = "update notice set Views=Views+1 where no =" + no;

                MySqlCommand updateCmd = new MySqlCommand(updateSQL, conn);

                if (updateCmd.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("Update Success!!");
                }
                else
                {
                    Console.WriteLine("Update Fail!!");
                }

                string selectSQL = "select * from notice where no=" + no;

                var model = new NoticeViewModel();

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
                }

                rdr.Close();

                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Debug.WriteLine("예외 디버깅: " + e.Message);
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
                string sql = "select * from notice order by No desc";

                var list = new List<BoardViewModel>();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var model = new NoticeViewModel();

                    model.No = (int)rdr["No"];
                    model.Email = (string)rdr["Email"];
                    model.Title = (string)rdr["Title"];
                    model.WriteDate = (string)rdr["WriteDate"];
                    model.Views = (int)rdr["Views"];

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
                string sql = "select * from notice where title like '%" + searchTxt + "%' order by No desc";

                var list = new List<BoardViewModel>();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var model = new NoticeViewModel();

                    model.No = (int)rdr["No"];
                    model.Email = (string)rdr["Email"];
                    model.Title = (string)rdr["Title"];
                    model.WriteDate = (string)rdr["WriteDate"];
                    model.Views = (int)rdr["Views"];

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

                var noticeVm = (NoticeViewModel)model;

                string sql;

                sql = "update notice set " +
                    "Title='" + noticeVm.Title + "', " +
                    "Content='" + noticeVm.Content + "' " +
                    "where No=" + noticeVm.No;

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

                var noticeVm = (NoticeViewModel)model;


                string sql = "insert into notice values(null, '"
                    + noticeVm.Title + "', '"
                    + noticeVm.Content + "', '"
                    + DateTime.Now.ToString("yyyy-MM-dd") + "', '"
                    + noticeVm.Email + "', 0)";

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

    }
}
