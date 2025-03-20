using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using BasicWebApp.Models;

namespace BasicWebApp.DataAccessLayer
{
    public class ProductDataAccess
    {
        //String connString = ConfigurationManager.ConnectionStrings["assignment2ConnString"].ToString();

        //public List<ProductCustomer> ListProduct()
        //{
        //    List<ProductCustomer> list = new List<ProductCustomer>();
        //    List<ProductInformation> listInformation = new List<ProductInformation>();
        //    List<CustomerData> listCustomer = new List<CustomerData>(); 
        //    SqlConnection sqlConnection = new SqlConnection(connString);
        //    try
        //    {
        //        sqlConnection.Open();
        //        String query = "DisplayProductCustomer";
        //        SqlCommand cmd = new SqlCommand(query, sqlConnection);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                list.Add(new ProductCustomer
        //                {
        //                    CustomerId = Convert.ToInt32(reader["customer_id"]),
        //                    ProductId = Convert.ToInt32(reader["product_id"]),
        //                });

        //                listCustomer.Add(new CustomerData
        //                {
        //                    CustomerId = Convert.ToInt32(reader["customer_id"]),
        //                    CustomerName = reader["customer_name"].ToString()

        //                });

        //                listInformation.Add(new ProductInformation
        //                {
        //                    ProductId = Convert.ToInt32(reader["product_id"]),
        //                    ProductName = reader["product_name"].ToString(),
        //                });
        //            }
        //            sqlConnection.Close();
        //        }

        //        sqlConnection.Close();
        //    }

        //    catch (Exception e)
        //    {
        //        sqlConnection.Close();
        //    }
        //    return list;
        //}

        public List<CustomerProductDetail> ListProductCustomerInfo()
        {
            List<CustomerProductDetail> list = new List<CustomerProductDetail>();
            String connString = ConfigurationManager.ConnectionStrings["assignment2ConnString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            try
            {
                sqlConnection.Open();
                String query = "DisplayProductCustomer";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new CustomerProductDetail
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            CustomerName = reader["CustomerName"].ToString(),
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            ProductName = reader["ProductName"].ToString()
                        });

                    }
                    sqlConnection.Close();
                }


            }
            catch (Exception e)
            {
                sqlConnection.Close();
            }
            return list;
        }

        public List<ProductInformation> listProducts()
        {
            List<ProductInformation> list = new List<ProductInformation>();
            String connString = ConfigurationManager.ConnectionStrings["assignment2ConnString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            try
            {
                sqlConnection.Open();
                String query = "DisplayProducts";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ProductInformation
                        {
                            ProductId = Convert.ToInt32(reader["ProductID"]),
                            ProductName = reader["ProductName"].ToString(),
                            ProductQuantity = Convert.ToInt32(reader["ProductQuantity"])
                        });


                    }
                    sqlConnection.Close();
                }


            }
            catch (Exception e)
            {
                sqlConnection.Close();
            }
            return list;
        }

        public Boolean InsertProduct(string name, int id, int quantity)
        {
            Boolean inserted = false;
            String connString = ConfigurationManager.ConnectionStrings["assignment2ConnString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            try
            {
                sqlConnection.Open();
                String getTotalItemsq = "GetTotalItems";
                String query = "AddProduct";
                id = 0;
                SqlCommand getcmd = new SqlCommand(getTotalItemsq, sqlConnection);
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                getcmd.CommandType = CommandType.StoredProcedure;
                getcmd.CommandTimeout = 0;
                using (SqlDataReader reader = getcmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader["Total_Items"]) + 1000;
                    }
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ProductName", name);
                cmd.Parameters.AddWithValue("@ProductQuantity", quantity);
                cmd.Parameters.AddWithValue("@ProductId", id++);
                cmd.ExecuteNonQuery();
                inserted = true;

                sqlConnection.Close();
            }

            catch (Exception e)
            {
                sqlConnection.Close();
            }

            return inserted;

        }


        public ProductInformation GetProductByID(int id)
        {
            string connString = ConfigurationManager.ConnectionStrings["assignment2ConnString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            ProductInformation prd = new ProductInformation();
            try
            {
                sqlConnection.Open();
                string query = "GetProductByID";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ProductId", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        prd.ProductId = Convert.ToInt32(reader["product_id"]);
                        prd.ProductQuantity = Convert.ToInt32(reader["product_quantity"]);
                        prd.ProductName = reader["product_name"].ToString();
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                sqlConnection.Close();
            }
            return prd;
        }

        public Boolean DeleteProduct(int id)
        {
            Boolean deleted = false;
            String connString = ConfigurationManager.ConnectionStrings["assignment2ConnString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            try
            {
                sqlConnection.Open();
                String query = "DeleteProduct";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);

                //ProductInformation prd = new ProductInformation();
                //prd = GetProductByID(id);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ProductId", id);



                cmd.ExecuteNonQuery();
                deleted = true;

                sqlConnection.Close();
            }

            catch (Exception e)
            {
                sqlConnection.Close();
            }

            return deleted;
        }

        public Boolean EditProduct(string name, int id, int quantity)
        {
            Boolean modified = false;
            String connString = ConfigurationManager.ConnectionStrings["assignment2ConnString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            try
            {
                sqlConnection.Open();
                String query = "UpdateProduct";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ProductId", id);
                cmd.Parameters.AddWithValue("@ProductName", name);
                cmd.Parameters.AddWithValue("@ProductQuantity", quantity);  

                cmd.ExecuteNonQuery();
                modified = true;
            }
            catch (Exception e)
            {
                sqlConnection.Close();
            }
            return modified;
        }
    }
}