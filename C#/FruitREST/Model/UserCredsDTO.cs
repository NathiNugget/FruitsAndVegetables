using Microsoft.AspNetCore.Mvc;

namespace FruitREST.Model
{
    public record UserCredsDTO([FromHeader] string Username, [FromHeader] string Password);
}
