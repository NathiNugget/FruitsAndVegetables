﻿using FruitREST.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
            byte[] file = System.IO.File.ReadAllBytes("newest_image.jpg");
            return File(file, "image/jpeg");
        }

        [HttpPost]
        public IActionResult Post([FromBody] ImageDTO dto)
        {
            try
            {
                string input = dto.bytes.TrimStart('b').Trim('\'');
                byte[] bytes = Convert.FromBase64String(input);
                Image image = Image.Load<Rgba32>(bytes);
                image.SaveAsJpeg("newest_image.jpg"); 
                return Ok(dto.bytes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
