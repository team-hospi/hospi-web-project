﻿using hospi_web_project.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Services
{
    public class ProductService
    {
        private MySqlConnection conn { get; set; }

        public ProductService(DBService dbService)
        {
            conn = dbService.GetConnection();
        }

        public List<ProductViewModel> GetProductList()
        {
            try
            {
                conn.Open();
                string sql = "select * from product";

                List<ProductViewModel> list = new();

                //ExecuteReader를 이용하여
                //연결 모드로 데이타 가져오기
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ProductViewModel vm = new();
                    vm.ProductCode = (int)rdr["ProductCode"];
                    vm.ProductName = (string)rdr["ProductName"];
                    vm.Price = (string)rdr["Price"];
                    vm.ProductInfo = (string)rdr["ProductInfo"];

                    list.Add(vm);
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

        public ProductViewModel GetProductInfo(string code)
        {
            try
            {
                conn.Open();
                string sql = "select * from product where ProductCode =" + Int32.Parse(code);

                ProductViewModel model = new();

                //ExecuteReader를 이용하여
                //연결 모드로 데이타 가져오기
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    model.ProductCode = (int)rdr["ProductCode"];
                    model.ProductName = (string)rdr["ProductName"];
                    model.Price = (string)rdr["Price"];
                    model.ProductInfo = (string)rdr["ProductInfo"];
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
    }
}
