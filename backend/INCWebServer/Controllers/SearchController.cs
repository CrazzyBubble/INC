using INCWebServer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INCWebServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private SearchPageService service;

        public SearchController(SearchPageService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            var films = service.GetGenres_Films().Result;
            return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("film={name}")]
        public ActionResult<string> GetFilmsByName(string name)
        {
            var films = service.GetFilmsByName(name).Result;
            return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("genre_name={name}")]
        public ActionResult<string> GetFilmsByGenre(string name)
        {
            var films = service.GetFilmsByGenre(name).Result;
            return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("studio_name={name}")]
        public ActionResult<string> GetFilmsByStudio(string name)
        {
            var films = service.GetFilmsByStudio(name).Result;
            return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("genre_id={id}")]
        public ActionResult<string> GetFilmsByGenreId(int id)
        {
            var films = service.GetFilmsByGenreId(id).Result;
            return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("studio_id={id}")]
        public ActionResult<string> GetFilmsByStudioId(int id)
        {
            var films = service.GetFilmsByStudioId(id).Result;
            return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("sorted_by_popularity")]
        public ActionResult<string> GetSortedFilmsByPopularity()
        {
            var films = service.GetSortedFilmsByPopularity().Result;
            return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("sorted_by_rating")]
        public ActionResult<string> GetSortedFilmsByRating()
        {
            var films = service.GetSortedFilmsByRating().Result;
            return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("sorted_by_release")]
        public ActionResult<string> GetSortedFilmsByRelease()
        {
            var films = service.GetSortedFilmsByRelease().Result;
            return Ok(JsonConvert.SerializeObject(films));
        }
    }
}
