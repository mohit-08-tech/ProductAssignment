using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Web;

namespace ProductAssignment.Models
{
    public class ProductQuery
    {
        string strcon = ConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
        public List<Product> LoadAllProduct()
        {
            var items = new List<Product>();
            try
            {
                using (var cnn = new SqlConnection(strcon))
                {
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "dbo.LoadAllData";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 300;
                        cnn.Open();
                        var reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
                        while (reader.Read())
                        {
                            var item = new Product();
                            item.ProductID = (int)reader["ProductID"];
                            item.ProductName = (string)reader["ProductName"];
                            item.Size = (string)reader["Size"];
                            item.Price = (double)reader["Price"];
                            item.MFGDate = (DateTime)reader["MFGDate"];
                            item.Category = (string)reader["Category"];

                            items.Add(item);
                        }
                        reader.Close();
                        cnn.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error {0}",ex.ToString());
            }
            return items;
        }

        public List<Product> SearchProduct(Product product)
        {
            var items = new List<Product>();
            try
            {
                StringBuilder sb = new StringBuilder();
                if (!String.IsNullOrEmpty(product.ProductName))
                {
                    sb.Append("ProductName ='" + product.ProductName + "'");
                }
                if (!String.IsNullOrEmpty(product.Size))
                {
                    sb.Append(" " + product.SearchWith + " Size = '" + product.Size + "'");
                }
                if (product.Price > 0)
                {
                    sb.Append(" " + product.SearchWith + " Price = '" + product.Price + "'");
                }
                if (product.MFGDate != null && product.MFGDate > SqlDateTime.MinValue.Value)
                {
                    sb.Append(" " + product.SearchWith + " MFGDate = '" + String.Format("{0:yyyy/MM/dd}", product.MFGDate) + "'");
                }
                if (!String.IsNullOrEmpty(product.Category))
                {
                    sb.Append(" " + product.SearchWith + " Category = '" + product.Category + "'");
                }

                using (var cnn = new SqlConnection(strcon))
                {
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "dbo.SearchProduct";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 300;

                        cmd.Parameters.Add(new SqlParameter
                        {
                            ParameterName = "@strSearch",
                            SqlDbType = SqlDbType.VarChar,
                            SqlValue = sb.ToString()
                        });
                        cnn.Open();
                        var reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
                        while (reader.Read())
                        {
                            var item = new Product();
                            item.ProductID = (int)reader["ProductID"];
                            item.ProductName = (string)reader["ProductName"];
                            item.Size = (string)reader["Size"];
                            item.Price = (double)reader["Price"];
                            item.MFGDate = (DateTime)reader["MFGDate"];
                            item.Category = (string)reader["Category"];
                            items.Add(item);
                        }
                        reader.Close();
                        cnn.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error {0}", ex.ToString());
            }
            return items;
        }
    }
}