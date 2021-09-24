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
                    model.IsPrivate = (int)rdr["IsPrivate"];

                    object tempFile = rdr["File"];
                    if (tempFile.GetType() != typeof(DBNull))
                    {
                        model.FileName = (string)rdr["FileName"];
                        model.RawData = (byte[])tempFile;
                    }
                    else
                    {
                        model.File = null;
                        model.FileName = null;
                    }

                    object tempReply = rdr["Reply"];
                    if (tempReply.GetType() != typeof(DBNull)) model.Reply = (string)tempReply;
                    else model.Reply = null;
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
                    
                }
                else
                {
                    
                }
                */

                sql = "update inquiry set " +
                    "Title='" + inquiryVm.Title + "', " +
                    "Content='" + inquiryVm.Content + "', " +
                    "IsPrivate=" + inquiryVm.IsPrivate + ", " +
                    "File=null " +
                    "where No=" + inquiryVm.No;

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

                long FileSize = inquiryVm.File.Length;
                byte[] rawData = new byte[FileSize];

                inquiryVm.File.OpenReadStream().Read(rawData, 0, (int)FileSize);
                inquiryVm.File.OpenReadStream().Close();
                
                string sql = "insert into inquiry values(null, '" 
                    + inquiryVm.Title + "', '"
                    + inquiryVm.Content + "', '"
                    + DateTime.Now.ToString("yyyy-MM-dd") +"', '"
                    + inquiryVm.Email +"', 0, " 
                    + "@File, '"
                    + inquiryVm.File.FileName + "', "
                    + inquiryVm.IsPrivate + ", 0, null)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlParameter blob = new MySqlParameter("@File", MySqlDbType.Blob, rawData.Length);
                blob.Value = rawData;

                cmd.Parameters.Add(blob);

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

        public void WriteReply(InquiryBoardViewModel model)
        {
            try
            {
                conn.Open();

                string sql = "update inquiry set IsReply=1, Reply='" + model.Reply + "' where No=" + model.No;

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

        public byte[] GetFileByte(int no)
        {
            try
            {
                conn.Open();

                string sql = "select File from inquiry where no=" + no;

                byte[] rawData = new byte[100];

                MySqlCommand selectCmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = selectCmd.ExecuteReader();
                while (rdr.Read())
                {
                    object tempFile = rdr["File"];
                    if (tempFile.GetType() != typeof(DBNull))
                    {
                        rawData = (byte[])tempFile;
                    }
                }

                rdr.Close();

                return rawData;
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
    }
}
