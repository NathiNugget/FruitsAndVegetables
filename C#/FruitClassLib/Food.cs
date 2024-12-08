﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FruitClassLib
{
    public class Food
    {
        private int _id;
        private string _name;
        private string _apiLink;
        private byte _spoilDate;
        private byte _spoilHours;
        private double _idealTemperature;
        private double _idealHumidity;
        private int _foodTypeId;
        private string _foodTypeName;

        public Food(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity, int id = 1, string foodTypeName = "")
        {
            FoodTypeId = foodTypeId;
            FoodTypeName = foodTypeName;
            Id = id;
            Name = name;
            ApiLink = apiLink;
            SpoilDate = spoilDate;
            SpoilHours = spoilHours;
            IdealTemperature = idealTemperature;
            IdealHumidity = idealHumidity;
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

        public byte SpoilDate { get => _spoilDate; set => _spoilDate = value; }

        public byte SpoilHours { get => _spoilHours; set => _spoilHours = value; }

        public double IdealTemperature { get => _idealTemperature;

            set
            {
                if (value < -50 || value > 85)
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
    }
}
