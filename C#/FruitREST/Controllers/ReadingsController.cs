using FruitClassLib;
using FruitREST.Authentication;
using FruitREST.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FruitREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class ReadingsController : ControllerBase
    {
        private IReadingsRepository _repo;

        public ReadingsController(IReadingsRepository readingsDB)
        {
            _repo = readingsDB;
        }

        // GET: api/<ReadingsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [EnableCors]
        public IActionResult Get([FromQuery] RangeDTO dto)
        {
            try
            {
                List<Reading> readings = _repo.Get(dto.offset, dto.count);
                if (readings.Count == 0)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(readings);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ReadingsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [EnableCors("PrivilegedPolicy")]

        public IActionResult Post([FromBody] ReadingDTO dto)
        {
            try
            {
                Reading reading = ReadingConverter.DTO2Reading(dto);
                Reading response = _repo.Add(reading);
                return Created($"/api/Readings/{response.Id}", response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                _repo.Setup();
                return Ok();
            }
            else
            {
                return StatusCode(401);
            }
        }

    }
}
