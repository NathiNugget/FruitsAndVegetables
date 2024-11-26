using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitClassLib
{
    public class ReadingsDB
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
            throw new NotImplementedException();
        }

        public List<Reading> Get(int offset, int count)
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
