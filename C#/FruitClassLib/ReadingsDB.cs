using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitClassLib
{
    public class ReadingsDB : IReadingsRepository
    {
        private string _connectionstring;
        public ReadingsDB(bool isTest)
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
        
        public Reading Add(Reading reading)
        {
            string query = "AddMeasurement";
            Reading readingToReturn = null!;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@temperature", reading.Temperature);
                cmd.Parameters.AddWithValue("@humidity", reading.Humidity);


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        
                        readingToReturn = ReadFromDB(reader);

                    }
                }
            }
            if (readingToReturn == null) throw new Exception("Reading could not be inserted into database");
            return readingToReturn;
        }

        public List<Reading> Get(int? offset, int? count)
        {
            string query = "GetMeasurements";
            List<Reading> readingList = new List<Reading>();
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(@query, connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@lowInterval", offset);
                cmd.Parameters.AddWithValue("@highInterval", count);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Reading reading = ReadFromDB(reader); 
                        readingList.Add(reading);
                    }
                }
            }
            return readingList;

        }

        private Reading ReadFromDB(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            long timestamp = reader.GetInt64(1);
            double temperature = reader.GetDouble(2);
            double humidity = reader.GetDouble(3);
            return new Reading(temperature, humidity, id, timestamp);
        }

        public IEnumerable<object> GetAll()
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
                Console.WriteLine($"I am become death, destroyer of universes. Rows nuked: {nuked}");
            }

        }

        public void Setup()
        {
#if !DEBUG
            return;
#endif

            Reading _reading = new Reading(50, 50);
            for (int i = 0; i < 15; i++)
            {
                Add(_reading);
            }
        }
    }
}
