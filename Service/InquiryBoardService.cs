using hospi_web_project.Models;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Service
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
            throw new NotImplementedException();
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

                    object temp = rdr["File"];
                    if (temp.GetType() != typeof(DBNull)) model.File = (IFormFile)temp;
                    else model.File = null;

                    model.Private = (int)rdr["Private"];
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
                    model.Content = (string)rdr["Content"];
                    model.WriteDate = (string)rdr["WriteDate"];
                    model.Views = (int)rdr["Views"];

                    object temp = rdr["File"];
                    if (temp.GetType() != typeof(DBNull)) model.File = (IFormFile)temp;
                    else model.File = null;

                    model.Private = (int)rdr["Private"];

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

        public List<BoardViewModel> SearchBoard(string searchText)
        {
            throw new NotImplementedException();
        }

        public int UpdateBoard(BoardViewModel model)
        {
            throw new NotImplementedException();
        }

        public void WriteBoard(BoardViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
