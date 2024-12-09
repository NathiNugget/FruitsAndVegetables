using FruitREST.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FruitREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("PrivilegedPolicy")]
    public class ImagesController : ControllerBase
    {
        public ImagesController() { }
        [HttpGet]
        public IActionResult Get()
        {
            byte[] file = System.IO.File.ReadAllBytes("./somefile.jpg");


            return File(file, "image/jpeg");
        }

        [HttpPost]
        public IActionResult Post([FromBody] ImageDTO dto)
        {
            try
            {

                string input = dto.bytes.TrimStart('b').Trim('\'');
                Console.WriteLine(input);
                byte[] bytes = Convert.FromBase64String(input);
                Image image = Image.Load<Rgba32>(bytes);
                image.SaveAsJpeg("somefile.jpg");

                Console.WriteLine("END");
                return Ok(dto.bytes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }


    }
}
