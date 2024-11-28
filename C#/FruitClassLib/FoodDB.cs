using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace FruitClassLib
{
    public class FoodDB : IFoodDB
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
            string query = "INSERT INTO Fruits (DanishName, IsVegetable, ApiMapping, SpoilDays, SpoilHours, IdealTemperature, IdealHumidity) " + 
                "OUTPUT inserted.Id, inserted.DanishName, inserted.IsVegetable, inserted.ApiMapping, inserted.SpoilDays, inserted.SpoilHours, inserted.IdealTemperature, inserted.IdealHumidity " +
                "VALUES (@name, @isVegetable, @apiLink, @spoilDate, @spoilHours, @temperature, @humidity)";
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", food.Name);
                cmd.Parameters.AddWithValue("@isVegetable", food.IsVegetable);
                cmd.Parameters.AddWithValue("@apiLink", food.ApiLink);
                cmd.Parameters.AddWithValue("@spoilDate", food.SpoilDate);
                cmd.Parameters.AddWithValue("@spoilHours", food.SpoilHours);
                cmd.Parameters.AddWithValue("@temperature", food.IdealTemperature);
                cmd.Parameters.AddWithValue("@humidity", food.IdealHumidity);


                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        bool isVegetable = reader.GetBoolean(2);
                        string apiLink = reader.GetString(3);
                        byte spoilDays = reader.GetByte(4);
                        byte spoilHours = reader.GetByte(5);
                        double idealTemperature = reader.GetDouble(6);
                        double idealHumidity = reader.GetDouble(7);
                        return new Food(name, isVegetable, apiLink, spoilDays, spoilHours, idealTemperature, idealHumidity,id);
                        
                    }
                }

            }
            return food;

        }

        public Food FindByName(string name)
        {
            string query = "SELECT * FROM Fruits WHERE DanishName = @Name";
            Food foodToReturn = null;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open(); SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string danishName = reader.GetString(1);
                        bool isVegetable = reader.GetBoolean(2);
                        string apiLink = reader.GetString(3);
                        byte spoilDays = reader.GetByte(4);
                        byte spoilHours = reader.GetByte(5);
                        double idealTemperature = reader.GetDouble(6);
                        double idealHumidity = reader.GetDouble(7);
                        foodToReturn = new Food(danishName, isVegetable, apiLink, spoilDays, spoilHours, idealTemperature, idealHumidity, id);
                    }
                }
            }
            if (foodToReturn == null)
            {
                throw new KeyNotFoundException($"No food found with the name: {name}");
            }
            return foodToReturn;
        }

        public List<Food> GetAll(bool? filterFruit = null, bool? filterVegetable = null)
        {
            string query = "GetFruits";

            List<Food> listOfFood = new List<Food>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@filterFruit", filterFruit);
                cmd.Parameters.AddWithValue("@filterVegetable", filterVegetable);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        bool isVegetable = reader.GetBoolean(2);
                        string apiLink = reader.GetString(3);
                        byte spoilDays = reader.GetByte(4);
                        byte spoilHours = reader.GetByte(5);
                        double idealTemperature = reader.GetDouble(6);
                        double idealHumidity = reader.GetDouble(7);
                        Food foodToReturn = new Food(name, isVegetable, apiLink, spoilDays, spoilHours, idealTemperature, idealHumidity, id);
                        listOfFood.Add(foodToReturn);
                    }
                }
            }
            return listOfFood;
        }

        public List<string> GetAllNames()
        {
            string query = "SELECT DanishName FROM Fruits";
            List<string> listOfNames = new List<string>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(0);
                        listOfNames.Add(name);
                    }
                }
            }
            return listOfNames;
        }

        public void Nuke()
        {
#if !DEBUG
            return;
#endif
            //TODO storeprocedure til NukeFood
            string query = "NukeFruits";
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
            Food food = new Food("Æble", false, "Apple.link", (byte)2, (byte)20, 23.0, 50.0);
            Food potato = new Food("Kartofel", true, "Potato.link", (byte)2, (byte)20, 23.0, 50.0);
            Food cucumber = new Food("Agurk", true, "Cucumber.link", (byte)2, (byte)20, 23.0, 50.0);
            Add(cucumber);
            Add(potato);
            Add(food);
        }
    }
}
