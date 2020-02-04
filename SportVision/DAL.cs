using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SportVision
{
    public class DAL
    {
        string StrCon = ConfigurationManager.ConnectionStrings["LIVEDNS"].ConnectionString;
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader reader = null;

        public DAL()
        {
            con = new SqlConnection(StrCon);
            com = new SqlCommand();
            com.Connection = con;
        }

        public User CheckUserIfExistAfterFBLogin(string id,string email,string username,string imageURL)
        {
            try
            {
                con.Open();
                com.CommandText = $"SELECT * FROM FinalProject_Kasem_UsersTB WHERE Email = '{email}' AND Facebook_ID = '{id}'";
                reader = com.ExecuteReader();
                if (reader.Read())
                {
                    User user = new User()
                    {
                        UserID = int.Parse(reader["User_ID"].ToString()),
                        FacebookID = id,
                        Username = username,
                        Email = email,
                        ImageURL = reader["ProfilePIC"].ToString()
                    };
                    reader.Close();
                    return user;
                }
                else
                {
                    reader.Close();

                    User user = new User()
                    {
                        FacebookID = id,
                        Username = username,
                        Email = email,
                        ImageURL = imageURL
                    };

                    
                    if (user.Email == "")
                        return null;

                    com.CommandText = "INSERT INTO FinalProject_Kasem_UsersTB(Username,Email,Facebook_ID,ProfilePIC)"
                            +" Values(@param1,@param2,@param3,@param4)";
                 
                    com.Parameters.Add("param1", SqlDbType.NVarChar).Value=user.Username;
                    com.Parameters.Add("param2", SqlDbType.VarChar).Value=user.Email;
                    com.Parameters.Add("param3", SqlDbType.VarChar).Value=user.FacebookID;
                    com.Parameters.Add("param4", SqlDbType.VarChar).Value = user.ImageURL;
                    int result = com.ExecuteNonQuery();
                    if (result != 1)
                        throw new Exception("Something went wrong");

                    com.CommandText = $"SELECT * FROM FinalProject_Kasem_UsersTB WHERE Email = '{email}' AND Facebook_ID = '{id}'";
                    reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        user.UserID = int.Parse(reader["User_ID"].ToString());
                        reader.Close();
                        return user;
                    }
                    else
                        return null;
                    
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                if (reader != null)
                {
                    if (!reader.IsClosed)
                        reader.Close();
                }
            }
        }


        public User CheckUserIfExistAfterGoogleLogin(string id, string email, string username,string imageURL)
        {
            try
            {
                con.Open();
                com.CommandText = $"SELECT * FROM FinalProject_Kasem_UsersTB WHERE Email = '{email}' AND Google_ID = '{id}'";
                reader = com.ExecuteReader();
                if (reader.Read())
                {
                    User user = new User()
                    {
                        UserID = int.Parse(reader["User_ID"].ToString()),
                        GoogleID = id,
                        Username = username,
                        Email = email,
                        ImageURL = reader["ProfilePIC"].ToString()
                    };
                    reader.Close();
                    return user;
                }
                else
                {
                    reader.Close();

                    User user = new User()
                    {
                        GoogleID = id,
                        Username = username,
                        Email = email,
                        ImageURL = imageURL
                    };


                    if (user.Email == "")
                        return null;

                    com.CommandText = "INSERT INTO FinalProject_Kasem_UsersTB(Username,Email,Google_ID,ProfilePIC)"
                            + " Values(@param1,@param2,@param3,@param4)";

                    com.Parameters.Add("param1", SqlDbType.NVarChar).Value = user.Username;
                    com.Parameters.Add("param2", SqlDbType.VarChar).Value = user.Email;
                    com.Parameters.Add("param3", SqlDbType.VarChar).Value = user.GoogleID;
                    com.Parameters.Add("param4", SqlDbType.VarChar).Value = user.ImageURL;
                    int result = com.ExecuteNonQuery();
                    if (result != 1)
                        throw new Exception("Something went wrong");

                    com.CommandText = $"SELECT * FROM FinalProject_Kasem_UsersTB WHERE Email = '{email}' AND Facebook_ID = '{id}'";
                    reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        user.UserID = int.Parse(reader["User_ID"].ToString());
                        reader.Close();
                        return user;
                    }
                    else
                        return null;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                if (reader != null)
                {
                    if (!reader.IsClosed)
                        reader.Close();
                }
            }
        }
    }
}