using INCWebServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;

namespace INCWebServer.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MediaController : Controller
    {
        private string filmSrc = @"~\content\films\";
        private readonly SubsidiaryService subservice;

        public MediaController(SubsidiaryService subservice)
        {
            this.subservice = subservice;
        }

        [HttpGet("{file_name}")]
        public IActionResult GetFilm(string file_name)
        {
            if (file_name == null || file_name == "")
                return BadRequest();
            using (FileStream fs = new FileStream("C:\\Content\\films\\" + file_name, FileMode.Open))
            {
                return new FileStreamResult(fs, new MediaTypeHeaderValue("video/mp4").MediaType);
            }
            //return BadRequest();
        }
    }
}
