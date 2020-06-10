using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INCWebServer.Services;
using INCWebServer.Sources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INCWebServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private SearchPageService searchService;

        public SearchController(SearchPageService service)
        {
            searchService = service;
        }
        [HttpGet]
        public ActionResult<string> Get()
        {
            var films = searchService.GetGenres_Films().Result;
            return Ok(JsonConvert.SerializeObject(films));
        }
        [HttpGet("film={name}")]
        public ActionResult<string> GetFilmsByName(string name)
        {
            var films = searchService.GetFilmsByName(name).Result;
            return Ok(JsonConvert.SerializeObject(films));
        }
        [HttpGet("genre={name}")]
        public ActionResult<string> GetFilmsByGenre(string name)
        {
            var films = searchService.GetFilmsByGenre(name).Result;
            return Ok(JsonConvert.SerializeObject(films));
        }
        [HttpGet("sorted_by_popularity")]
        public ActionResult<string> GetSortedFilmsByPopularity()
        {
            var films = searchService.GetSortedFilmsByPopularity().Result;
            return Ok(JsonConvert.SerializeObject(films));
        }
        [HttpGet("sorted_by_rating")]
        public ActionResult<string> GetSortedFilmsByRating()
        {
            var films = searchService.GetSortedFilmsByRating().Result;
            return Ok(JsonConvert.SerializeObject(films));
        }
        [HttpGet("sorted_by_release")]
        public ActionResult<string> GetSortedFilmsByRelease()
        {
            var films = searchService.GetSortedFilmsByRelease().Result;
            return Ok(JsonConvert.SerializeObject(films));
        }
    }
}
