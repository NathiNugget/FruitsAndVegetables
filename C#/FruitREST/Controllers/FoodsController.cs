using FruitClassLib;
using FruitREST.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FruitREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class FoodsController : ControllerBase
    {
        private IFoodDB _foodDB;

        public FoodsController(IFoodDB foodDB) 
        {
            _foodDB = foodDB;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get([FromQuery] FoodFilterDTO filter)
        {
            try
            {
                List<Food> foods = _foodDB.GetAll(filter.filterFruit, filter.filterVegetable);
                if (foods.Count == 0)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(foods);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }
        [HttpGet("byName={name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get(string name)
        {
            try
            {
                Food food = _foodDB.FindByName(name);
                return Ok(food);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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

        public IActionResult Post([FromBody] FoodDTO dto)
        {
            try
            {
                Food food = FoodDTOConverter.DTO2Food(dto);
                Food response = _foodDB.Add(food);
                return Created($"/api/Foods/{response.Id}", response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Names")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetNames([FromQuery] FoodFilterDTO filter)
        {
            try
            {
                List<string> names = _foodDB.GetAllNames(filter.filterFruit, filter.filterVegetable);
                if (names.Count == 0)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(names);
                }
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
                _foodDB.Nuke();
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
                _foodDB.Setup();
                return Ok();
            }
            else
            {
                return StatusCode(401);
            }
        }
    }
}
