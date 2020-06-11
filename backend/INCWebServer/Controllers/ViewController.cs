using INCWebServer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INCWebServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewController : ControllerBase
    {
        ViewPageService service;
        public ViewController(ViewPageService service)
        {
            this.service = service;
        }

        [HttpGet("film={id}")]
        public ActionResult<string> GetFilmsByName(int id)
        {
            var films = service.GetFilmById(id).Result;
            return Ok(JsonConvert.SerializeObject(films));
        }
    }
}
