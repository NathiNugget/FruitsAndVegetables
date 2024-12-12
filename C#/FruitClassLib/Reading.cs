namespace FruitClassLib
{
    public class Reading
    {
        private int _id;
        private double _temperature;
        private double _humidity;
        private long _timestamp;

        public Reading(double temperature, double humidity, int id = 1, long timestamp = 0)
        {
            Temperature = temperature;
            Humidity = humidity;
            Id = id;
            Timestamp = timestamp;
        }

        public double Temperature
        {
            get { return _temperature; }
            set
            {
                if (value < -50 || value > 85)
                {
                    throw new ArgumentOutOfRangeException($"Temperature has to be between -50 and 85. Passed: {value}");
                }
                _temperature = value;
            }
        }
        public double Humidity
        {
            get { return _humidity; }
            set
            {
                if (value > 100.0 || value < 0.0)
                {
                    throw new ArgumentOutOfRangeException($"Humidity has to be between 0.0 and 100.0. Passed: {value}");
                }
                _humidity = value;
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException($"ID has to be at least 1. Passed: {value}");
                _id = value;
            }
        }

        public long Timestamp
        {
            get { return _timestamp; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException($"Time in Epoch milliseconds has to be at least 0 (year 1970). Passed: {value}");
                _timestamp = value;
            }
        }





    }
}
