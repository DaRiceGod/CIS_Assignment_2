using BasicWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace BasicWebApp.DataAccessLayer
{
    public class UserDataAccess
    {
        public Boolean GetUser(string user, string pass)
        {
            String connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            try
            {
                sqlConnection.Open();
                String query = "Select * FROM CardsHolder WHERE Name = '" + user + "' AND Password = '" + pass + "'";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 0;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                }
                sqlConnection.Close();


            }
            catch (Exception e) 
            { 
                sqlConnection.Close(); 
            }
            return false;

        }

        public Boolean ResetPassword(string user)
        {
            String connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            try
            {
                sqlConnection.Open();
                String query = "Select * FROM CardsHolder WHERE Name = '" + user + "'";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 0;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                }
                sqlConnection.Close();


            }
            catch (Exception e)
            {
                sqlConnection.Close();
            }
            return false;
        }

        public List<USER> GetUserList()
        {

            List<USER> users = new List<USER>();
            String connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            try
            {
                sqlConnection.Open();
                String query = "GetUserInfo";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new USER
                        {
                            password = reader["Password"].ToString(),
                            userName = reader["Name"].ToString()
                        }

                        );

                    }
                    sqlConnection.Close();
                }


            }
            catch (Exception e)
            {
                sqlConnection.Close();
            }
            return users;

        }

        public void AddUser(USER user)
        {
            String connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            try
            {
                sqlConnection.Open();
                String query = "INSERT INTO CardsHolder (Name, Password) VALUES ('" + user.userName + "', '" + user.password + "')";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                sqlConnection.Close();
            }
        }

        public Boolean InsertProduct(String name, String password)
        {
            Boolean inserted = false;
            //List<CustomareProductDTO> customareprd = new List<CustomareProductDTO>();

            String connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            try
            {
                sqlConnection.Open();
                String query = "AddUser";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Pass", password);
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

    }



}