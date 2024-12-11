using FruitClassLib;
using FruitREST.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FruitREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class UsersController : ControllerBase
    {
        private IUserDB _repo;

        public UsersController(IUserDB userDB)
        {
            _repo = userDB;
        }
        [HttpDelete]
        [Route("nuke")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [EnableCors("PrivilegedPolicy")]
        public IActionResult Nuke()
        {
            if (TestMode.TestModeIsDev)
            {
                _repo.Nuke();
                return Ok();
            }
            else
            {
                return StatusCode(401);
            }

        }

        [HttpPost]
        [Route("setup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [EnableCors("PrivilegedPolicy")]
        public IActionResult Setup()
        {
            if (TestMode.TestModeIsDev)
            {
                _repo.SetUp();
                return Ok();
            }
            else
            {
                return StatusCode(401);
            }
        }

        [HttpGet("getbycredentials")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetByCreds(UserCredsObject userCreds)
        {
            User? user = _repo.Get(userCreds.Username, userCreds.Password);
            if (user == null)
            {
                return StatusCode(401);
            }
            else
            {
                return Ok(user);
            }

        }

        [HttpGet("getnewsessiontoken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetSessionTokenByCreds(UserCredsObject userCreds)
        {
            string? token = _repo.GetNewSessionToken(userCreds.Username, userCreds.Password);
            if (token == null)
            {
                return StatusCode(401);
            }
            else
            {
                return Ok(token);
            }

        }

        [HttpGet("validatetoken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetSessionTokenByCreds([FromHeader] string token)
        {
            bool validated = _repo.Validate(token);
            if (!validated)
            {
                return StatusCode(401);
            }
            else
            {
                return Ok(token);
            }
        }

        [HttpPut("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult LogOutWithToken([FromHeader] string token)
        {
            bool validated = _repo.ResetSessionToken(token);
            if (!validated)
            {
                return StatusCode(401);
            }
            else
            {
                return Ok(token);
            }

        }


    }
}
