using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitClassLib
{
    public class User
    {
        static readonly HashSet<char> DISALLOWED_PASSWORD_CHARS = ['-'];

        static readonly HashSet<char> DISALLOWED_NAME_CHARS = ['-', ',', '´'];

        const int MAX_NAME_LENGTH = 50;

        const int MIN_NAME_LENGTH = 2;

        const int MAX_PASSWORD_LENGTH = 256;

        const int MIN_PASSWORD_LENGTH = 5;

        public User(string name, string password, int id = 1, string? sessionToken = null)
        {
            Name = name;
            Password = password;
            Id = id;
            SessionToken = sessionToken;
        }
        private string _name;

        private string _password;


        public string? SessionToken { get; set; }

        public int Id { get; set; }

        public string Name 
        { 
            get 
            {  
                return _name; 
            } 
            set
            {
                ValidateName(value);
                _name = value;
            }
        
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                ValidatePassword(value);
                _password = value;
            }

        }


        private void ValidateName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"Name cannot be null, empty or consist only of whitespace characters");
            }
            else if (name.Length < MIN_NAME_LENGTH || name.Length > MAX_NAME_LENGTH)
            {
                throw new ArgumentException($"Name must not be less than than {MIN_NAME_LENGTH} characters, and must not be more than {MAX_NAME_LENGTH} characters");
            }
            else if (DISALLOWED_NAME_CHARS.Any(s => name.Contains(s)))
            {
                throw new ArgumentException($"Name {name} contains invalid characters");
            }
        }


        private void ValidatePassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException($"Password cannot be null, empty or consist only of whitespace characters");
            }
            else if (password.Length < MIN_PASSWORD_LENGTH || password.Length > MAX_PASSWORD_LENGTH)
            {
                throw new ArgumentException($"Password must not be less than than {MIN_PASSWORD_LENGTH} characters, and must not be more than {MAX_PASSWORD_LENGTH} characters");
            }
            else if (DISALLOWED_PASSWORD_CHARS.Any(s => password.Contains(s)))
            {
                throw new ArgumentException($"Name {password} contains invalid characters");
            }
        }
    }
}
