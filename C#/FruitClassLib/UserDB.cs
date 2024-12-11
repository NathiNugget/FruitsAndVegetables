using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FruitClassLib
{
    public class UserDB : IUserDB
    {
        private string _connectionstring;

        public UserDB(bool isTest)
        {
            if (isTest)
            {
                _connectionstring = Secret.SecretKey.ConnectionStringTest;

            }
            else
            {
                _connectionstring = Secret.SecretKey.ConnectionStringProduction;
            }
        }

        public User Add(User user)
        {

            string procedure = "AddUser";

            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();


                SqlCommand cmd = new SqlCommand(procedure, conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@password", user.Password);


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ReadUserItem(reader);

                    }
                }


            }
            throw new Exception("Error, something went wrong");
        }

        public User? Get(string username, string password)
        {
            if (username == null || password == null)
            {
                return null;
            }
            string procedure = "GetUserByNameAndPassword";

            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();


                SqlCommand cmd = new SqlCommand(procedure, conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@name", username);
                cmd.Parameters.AddWithValue("@password", password);


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ReadUserItem(reader);

                    }
                    else
                    {
                        return null;
                    }
                }


            }
            throw new Exception("Error, something went wrong");
        }
        public string? GetNewSessionToken(string username, string password)
        {
            if (username == null || password == null) {
                return null;
            }
            string procedure = "GenerateSessionToken";

            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();


                SqlCommand cmd = new SqlCommand(procedure, conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@name", username);
                cmd.Parameters.AddWithValue("@password", password);


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);

                    }
                    else
                    {
                        return null;
                    }
                }


            }
            throw new Exception("Error, something went wrong");
        }




        public bool ResetSessionToken(string sessionToken)
        {
            if (sessionToken is null)
            {
                return false;
            }
            string procedure = "LogOut";

            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();


                SqlCommand cmd = new SqlCommand(procedure, conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@sessiontoken", sessionToken);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }


            }
            throw new Exception("Error, something went wrong");
        }




        public void Nuke()
        {
#if !DEBUG
            return;
#endif
            //TODO storeprocedure til NukeFood
            string query = "NukeUsers";
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                int nuked = cmd.ExecuteNonQuery();
                Console.WriteLine($"I am Jimmy, destroyer of the universe. Rows nuked: {nuked} but i am just a chill guy");
            }
        }

        public void SetUp()
        {
            Add(new User("Marius", "hehehehe"));
            Add(new User("Jacob", "Hahaxd"));
            Add(new User("Isak", "isakooo"));
            Add(new User("Nathaniel", "maaguyyy"));
        }

        public bool Validate(string sessionToken)
        {
            if (sessionToken == null)
            {
                return false;
            }
            string procedure = "ValidateSessionToken";

            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();


                SqlCommand cmd = new SqlCommand(procedure, conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@sessionToken", sessionToken);



                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }


            }
            throw new Exception("Error, something went wrong");
        }


        private User ReadUserItem(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string password = reader.GetString(2);
            string? sessionToken = reader.IsDBNull(3) ? null : reader.GetString(3);
            return new User(name, password, id, sessionToken);
        }


    }
}
