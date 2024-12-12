using Microsoft.AspNetCore.Mvc;

namespace FruitREST.Model
{
    public class UserCredsObject
    {
        [FromHeader]
        public string Password { get; set; }
        [FromHeader]

        public string Username { get; set; }
    }
}
