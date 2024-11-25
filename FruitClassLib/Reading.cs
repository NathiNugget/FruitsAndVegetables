namespace FruitClassLib
{
    public class Reading
    {
        public Reading(double temperature, double humidity, int id, long timestamp = 0)
        {
            Temp = temperature;
            Humidity = humidity;
            Id = id;
            Timestamp = timestamp;
        }

        public double Temp { get; }
        public double Humidity { get; }

        public int? Id { get; set; }
        public long Timestamp { get; set;  }


        

        
    }
}
