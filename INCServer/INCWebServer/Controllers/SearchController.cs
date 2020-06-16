using INCWebServer.Services;
using INCWebServer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace INCWebServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly SearchService service;
        private readonly SubsidiaryService subservice;

        public SearchController(SearchService service, SubsidiaryService subservice)
        {
            this.service = service;
            this.subservice = subservice;
        }

        [HttpGet]
        public IActionResult GetFilmsByGenre()
        {
            ViewData["Genres"] = subservice.GetUsefullGenres().Result;
            var films = service.GetGenres_Films().Result;
            return View("SearchMain", films);
            //return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("films")]
        public IActionResult GetFilmsByOneParameter()
        {
            ViewData["Genres"] = subservice.GetUsefullGenres().Result;
            ViewData["ParameterName"] = "none";
            if (Request.Query.Keys.Count > 1)
                return BadRequest("A lot of parameters");
            string[] keys = { "film", "genre", "studio", "genreid", "studioid" };
            List<FilmInfoPromo> result = new List<FilmInfoPromo>();
            for (int i = 0; i < keys.Length; ++i)
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
                ViewData["ParameterName"] = keys[i];
                break;
            }
            return View("SearchDefault", result);
            /*if (result.Count == 0)
                return NoContent();*/
            //return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpGet("sorted_by_popularity")]
        public IActionResult GetSortedFilmsByPopularity()
        {
            ViewData["Genres"] = subservice.GetUsefullGenres().Result;
            var films = service.GetSortedFilmsByPopularity().Result;
            if (films == null)
                return NoContent();
            ViewData["TypeOperation"] = "popularity";
            return View("SearchSorted", films);
            //return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("sorted_by_rating")]
        public IActionResult GetSortedFilmsByRating()
        {
            ViewData["Genres"] = subservice.GetUsefullGenres().Result;
            var films = service.GetSortedFilmsByRating().Result;
            if (films == null)
                return NoContent();
            ViewData["TypeOperation"] = "rating";
            return View("SearchSorted", films);
            //return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("sorted_by_release")]
        public IActionResult GetSortedFilmsByRelease()
        {
            ViewData["Genres"] = subservice.GetUsefullGenres().Result;
            var films = service.GetSortedFilmsByRelease().Result;
            if (films == null)
                return NoContent();
            ViewData["TypeOperation"] = "release";
            return View("SearchSorted", films);
            //return Ok(JsonConvert.SerializeObject(films));
        }
    }
}
