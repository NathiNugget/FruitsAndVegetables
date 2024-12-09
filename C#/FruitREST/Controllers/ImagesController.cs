using FruitREST.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;


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
            return Ok("Nice");
        }

        [HttpPost]
        public IActionResult Post([FromBody] ImageDTO dto)
        {
            try
            {

                string input = dto.bytes.TrimStart('b').Trim('\'');
                Console.WriteLine(input);
                byte[] bytes = Convert.FromBase64String(input);
                Image image = byteArrayToImage(bytes);
                image.Save("somefile.png", ImageFormat.Png);

                Console.WriteLine("END");
                return Ok(dto.bytes);
            }
            catch
            {

                return BadRequest();
            }
        }



        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
