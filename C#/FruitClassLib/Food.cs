namespace FruitClassLib
{
    public class Food
    {

        const double MAX_POSSIBLE_TEMPERATURE = 85;

        const double MIN_POSSIBLE_TEMPERATURE = -50;

        private int _id;
        private string _name;
        private string _apiLink;
        private byte _spoilDate;
        private byte _spoilHours;
        private double _q10Factor;
        private double _maxTemp;
        private double _minTemp;
        private double _idealTemperature;
        private double _idealHumidity;
        private int _foodTypeId;
        private string _foodTypeName;

        public Food(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity, int id = 1, string foodTypeName = "", double q10Factor = 3, double maxTemp = 40, double minTemp = -15   )
        {
            FoodTypeId = foodTypeId;
            FoodTypeName = foodTypeName;
            MaxTemp = maxTemp;
            Id = id;
            Name = name;
            ApiLink = apiLink;
            SpoilDate = spoilDate;
            SpoilHours = spoilHours;
            IdealTemperature = idealTemperature;
            IdealHumidity = idealHumidity;
            Q10Factor = q10Factor;
            MaxTemp = maxTemp;
            MinTemp = minTemp;
        }

        public int FoodTypeId
        {
            get => _foodTypeId;

            set
            {
                _foodTypeId = value;
            }
        }

        public string FoodTypeName
        {
            get => _foodTypeName;

            set
            {
                _foodTypeName = value;
            }
        }

        public double MaxTemp {
            get => _maxTemp;
            set
            {
                if (value > MAX_POSSIBLE_TEMPERATURE || value < MIN_POSSIBLE_TEMPERATURE)
                {
                    throw new ArgumentOutOfRangeException($"Max temperature has to be between {MIN_POSSIBLE_TEMPERATURE} and {MAX_POSSIBLE_TEMPERATURE}. Passed: {value}");
                }
                _maxTemp = value;
            }
        }


        public double MinTemp
        {
            get => _minTemp;
            set
            {
                if (value > MAX_POSSIBLE_TEMPERATURE || value < MIN_POSSIBLE_TEMPERATURE)
                {
                    throw new ArgumentOutOfRangeException($"Max temperature has to be between {MIN_POSSIBLE_TEMPERATURE} and {MAX_POSSIBLE_TEMPERATURE}. Passed: {value}");
                }
                _minTemp = value;
            }
        }




        public int Id { get => _id;

            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException($"ID has to be at least 1. passed{value}");
                }
                _id = value;
            }
        }
        public string Name { get => _name; 
            
            set
            {
                if(value.Length < 1 || value.Length > 30)
                {
                    throw new ArgumentException($"Name length has to be at least 1 and max 30 char long. passed {value.Length}");
                }
                _name = value;
            }
        
        }
        

        public string ApiLink { get => _apiLink; 
        
            set
            {
                if(value.Length < 1)
                {
                    throw new ArgumentNullException($"ApiLink can not be null. passed {value}");
                }
                _apiLink = value;
            }
        }

        public byte SpoilDate { get => _spoilDate; set =>  _spoilDate = value; }

        public byte SpoilHours { get => _spoilHours; set
            {
                if (value > 23)
                {
                    throw new ArgumentOutOfRangeException("SpoilHors must not be larger than 23");
                }
                _spoilHours = value;
            }
        }

        public double IdealTemperature { get => _idealTemperature;

            set
            {
                if (value < MIN_POSSIBLE_TEMPERATURE || value > MAX_POSSIBLE_TEMPERATURE)
                {
                    throw new ArgumentOutOfRangeException($"Temperature has to be between -50 and 85. Passed: {value}");
                }
                _idealTemperature = value;
            }
        }

        public double IdealHumidity { get => _idealHumidity;

            set
            {
                if (value > 100.0 || value < 0.0)
                {
                    throw new ArgumentOutOfRangeException($"Humidity has to be between 0.0 and 100.0. Passed: {value}");
                }
                _idealHumidity = value;
            }

        }

        public double Q10Factor
        {
            get => _q10Factor;

            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException($"Q10 has to be above 1. Passed: {value}");
                }
                _q10Factor = value;
            }
        }
    }
}
