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
            throw new NotImplementedException();
        }

        public Food FindByIsVeg(Food food)
        {
            throw new NotImplementedException();
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
#if DEBUG
            return;
#endif
            // TODO: Write implementation for nuking database.

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
