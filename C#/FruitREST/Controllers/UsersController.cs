using FruitClassLib;
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
    }
}
