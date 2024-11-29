﻿using Microsoft.Data.SqlClient;
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


            string query = "DECLARE @TypeName  NVARCHAR(20) SELECT @TypeName = TypeName  FROM FoodTypes WHERE FoodTypes.FoodId = @foodTypeId    INSERT INTO Fruits (DanishName, FoodTypeId, ApiMapping, SpoilDays, SpoilHours, IdealTemperature, IdealHumidity) \r\n\r\n     OUTPUT inserted.Id, inserted.DanishName, inserted.FoodTypeId, inserted.ApiMapping, inserted.SpoilDays, inserted.SpoilHours, inserted.IdealTemperature, inserted.IdealHumidity, @TypeName AS TypeName     VALUES (@name, @foodTypeId, @apiLink, @spoilDate, @spoilHours, @temperature, @humidity)";




            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();

                
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", food.Name);
                cmd.Parameters.AddWithValue("@foodTypeId", food.FoodTypeId);
                cmd.Parameters.AddWithValue("@apiLink", food.ApiLink);
                cmd.Parameters.AddWithValue("@spoilDate", food.SpoilDate);
                cmd.Parameters.AddWithValue("@spoilHours", food.SpoilHours);
                cmd.Parameters.AddWithValue("@temperature", food.IdealTemperature);
                cmd.Parameters.AddWithValue("@humidity", food.IdealHumidity);


                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                       return ReadFoodItem(reader);
                        
                    }
                }
                

            }
            throw new Exception("Error, something went wrong");

        }

        public Food FindByName(string name)
        {
            string query = "SELECT Fruits.Id AS FruitId, Fruits.DanishName, Fruits.FoodTypeId, Fruits.ApiMapping, Fruits.SpoilDays, Fruits.SpoilHours, Fruits.IdealTemperature, Fruits.IdealHumidity, FoodTypes.TypeName FROM Fruits JOIN FoodTypes ON Fruits.FoodTypeId = FoodTypes.FoodId WHERE DanishName = @Name";
            Food foodToReturn = null;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open(); SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        foodToReturn = ReadFoodItem(reader);
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
            string query = "GetFruitsJOIN";

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
                        Food foodToReturn = ReadFoodItem(reader);
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
            Food food = new Food("Æble", 1, "Apple.link", (byte)2, (byte)20, 23.0, 50.0);
            Food potato = new Food("Kartofel",2, "Potato.link", (byte)2, (byte)20, 23.0, 50.0);
            Food cucumber = new Food("Agurk",2, "Cucumber.link", (byte)2, (byte)20, 23.0, 50.0);
            Add(cucumber);
            Add(potato);
            Add(food);
        }

        private Food ReadFoodItem(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            int foodTypeId = reader.GetInt32(2);
            string apiLink = reader.GetString(3);
            byte spoilDays = reader.GetByte(4);
            byte spoilHours = reader.GetByte(5);
            double idealTemperature = reader.GetDouble(6);
            double idealHumidity = reader.GetDouble(7);
            string foodTypeName = reader.GetString(8);
            return new Food(name,foodTypeId, apiLink, spoilDays, spoilHours, idealTemperature, idealHumidity, id, foodTypeName);
        }
    }
}
