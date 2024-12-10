using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitClassLib
{
    public class UserDB : IUserDB
    {
        private string _connectionstring;

        public UserDB(bool isTest)
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

        public User? Get(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
