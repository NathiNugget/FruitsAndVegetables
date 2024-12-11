﻿using FruitClassLib;
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
        public IActionResult GetByCreds([FromHeader] UserCredsDTO credsDTO)
        {
            User? user = _repo.Get(credsDTO.username, credsDTO.password);
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
        public IActionResult GetSessionTokenByCreds([FromHeader] UserCredsDTO credsDTO)
        {
            string? token = _repo.GetNewSessionToken(credsDTO.username, credsDTO.password);
            if (token == null)
            {
                return StatusCode(401);
            }
            else
            {
                return Ok(token);
            }

        }

        [HttpGet("getnewsessiontoken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetSessionTokenByCreds([FromHeader] UserCredsDTO credsDTO)
        {
            string? token = _repo.GetNewSessionToken(credsDTO.username, credsDTO.password);
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
        public IActionResult GetSessionTokenByCreds([FromHeader] UserCredsDTO credsDTO)
        {
            string? token = _repo.GetNewSessionToken(credsDTO.username, credsDTO.password);
            if (token == null)
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