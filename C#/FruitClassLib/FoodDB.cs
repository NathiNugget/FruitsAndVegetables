using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitClassLib
{
    public class FoodDB
    {
        private string _connectionstring;
        public FoodDB(bool isTest)
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
        
        public Food Add(Food food)
        {
            string query = "AddFood";
            Food foodToReturn = null!;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", food.Name);
                cmd.Parameters.AddWithValue("@isVegetable", food.IsVegetable);
                cmd.Parameters.AddWithValue("@apiLink", food.ApiLink);
                cmd.Parameters.AddWithValue("@spoilDate", food.SpoilDate);
                cmd.Parameters.AddWithValue("@spoilHours", food.SpoilHours);
                cmd.Parameters.AddWithValue("@temperature", food.IdealTemperature);
                cmd.Parameters.AddWithValue("@humidity", food.IdealHumidity);


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        bool isVegetable = reader.GetBoolean(2);
                        string apiLink = reader.GetString(3);
                        byte spoilDate = reader.GetByte(4);
                        byte spoilHours = reader.GetByte(5);
                        double idealTemperature = reader.GetDouble(6);
                        double idealHumidity = reader.GetDouble(7);
                        foodToReturn = new Food (name, isVegetable, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity, id);

                    }
                }
            }
            if (foodToReturn == null) throw new Exception("Food could not be inserted into database");
            return foodToReturn;
        }

        public List<Food> FindByIsVegetable()
        {
            string query = "FindByIsVegetableFood";
            List<Food> listOfFood = new List<Food>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        bool isVegetable = reader.GetBoolean(2);
                        string apiLink = reader.GetString(3);
                        byte spoilDate = reader.GetByte(4);
                        byte spoilHours = reader.GetByte(5);
                        double idealTemperature = reader.GetDouble(6);
                        double idealHumidity = reader.GetDouble(7);
                        Food foodToReturn = new Food(name, isVegetable, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity, id);
                        listOfFood.Add(foodToReturn);

                    }
                    
                }
                

            }
            return listOfFood;

        }

        public Food FindByName(Food food)
        {
            throw new NotImplementedException();
        }

        public List<Food> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Nuke()
        {
#if !DEBUG
            return;
#endif

            string query = "NukeMeasurements";
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                int nuked = cmd.ExecuteNonQuery();
                Console.WriteLine($"I am Jimmy, destroyer of the universe. Rows nuked: {nuked} but i am just a chill guy");
            }

        }

        public void Setup()
        {
            Food Food = new Food("Banan", false, "Banan.link", (byte)2 , (byte)48, 23.0, 50.0);
            for (int i = 0; i < 15; i++)
            {
                Add(Food);
            }
        }
    }
}
