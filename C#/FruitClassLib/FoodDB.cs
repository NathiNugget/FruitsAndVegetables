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

        public Food Add(Food expected)
        {
            throw new NotImplementedException();
        }

        public Food FindByName(Food expected)
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
            Reading _reading = new Reading(50, 50);
            for (int i = 0; i < 15; i++)
            {
                Add(_reading);
            }
        }
    }
}
