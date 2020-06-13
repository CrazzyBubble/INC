using INCWebServer.Services;
using INCWebServer.Sources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INCWebServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("films")]
        public ActionResult<string> GetFilmsByOneParameter()
        {
            if (Request.Query.Keys.Count > 1)
                return BadRequest("A lot of parameters");
            string[] keys = { "film", "genre", "studio", "genreid", "studioid" };
            List<FilmInfoPromo> result = new List<FilmInfoPromo>();
            for(int i = 0; i < keys.Length; ++i)
            {
                if (!Request.Query.ContainsKey(keys[i]))
                    continue;
                string value = Request.Query.FirstOrDefault(p => p.Key == keys[i]).Value;
                switch(i)
                {
                    case 0:
                        result = service.GetFilmsByName(value).Result;
                        break;
                    case 1:
                        result = service.GetFilmsByGenre(value).Result;
                        break;
                    case 2:
                        result = service.GetFilmsByStudio(value).Result;
                        break;
                    case 3:
                        int id;
                        if (!Int32.TryParse(value, out id))
                            return BadRequest("Bad type");
                        result = service.GetFilmsByGenreId(id).Result;
                        break;
                    case 4:
                        if (!Int32.TryParse(value, out id))
                            return BadRequest("Bad type");
                        result = service.GetFilmsByStudioId(id).Result;
                        break;
                    default:
                        break;
                }
                break;
            }
            if (result.Count == 0)
                return NoContent();
            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpGet("sorted_by_popularity")]
        public ActionResult<string> GetSortedFilmsByPopularity()
        {
            var films = service.GetSortedFilmsByPopularity().Result;
            if (films == null)
                return NoContent();
            return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("sorted_by_rating")]
        public ActionResult<string> GetSortedFilmsByRating()
        {
            var films = service.GetSortedFilmsByRating().Result;
            if (films == null)
                return NoContent();
            return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("sorted_by_release")]
        public ActionResult<string> GetSortedFilmsByRelease()
        {
            var films = service.GetSortedFilmsByRelease().Result;
            if (films == null)
                return NoContent();
            return Ok(JsonConvert.SerializeObject(films));
        }
    }
}
