using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpPost("addcarimage")]
        public IActionResult AddCarImage([FromForm] IFormFile image, [FromForm] CarImage img)
        {
            var result = _carImageService.Add(image, img);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deletecarimg")]
        public IActionResult DeleteCarImage([FromForm] CarImage img)
        {
            var result = _carImageService.Delete(img);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("updatecarimg")]
        public IActionResult UpdateCarImage([FromForm] IFormFile image, [FromForm]CarImage img)
        {
            var result = _carImageService.Update(image, img);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getlistbycarid")]
        public IActionResult GetListByCarId(int id)
        {
            var result = _carImageService.GetCarListByCarID(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallcarsimage")]
        public IActionResult GetAllCarsImage()
        {
            var result = _carImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcarimagebyid")]
        public IActionResult Get(int id)
        {
            var result = _carImageService.FindByID(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
