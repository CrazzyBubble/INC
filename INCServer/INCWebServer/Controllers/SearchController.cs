using INCWebServer.Services;
using INCWebServer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace INCWebServer.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> GetFilmsByGenre()
        {
            ViewData["Genres"] = subservice.GetUsefullGenres().Result;
            var films = await service.GetGenres_Films();
            return View("SearchMain", films);
            //return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("films")]
        public async Task<IActionResult> GetFilmsByOneParameter()
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
                        result = await service.GetFilmsByName(value);
                        break;
                    case 1:
                        result = await service.GetFilmsByGenre(value);
                        break;
                    case 2:
                        result = await service.GetFilmsByStudio(value);
                        break;
                    case 3:
                        int id;
                        if (!Int32.TryParse(value, out id))
                            return BadRequest("Bad parameter type");
                        result = await service.GetFilmsByGenreId(id);
                        break;
                    case 4:
                        if (!Int32.TryParse(value, out id))
                            return BadRequest("Bad parameter type");
                        result = await service.GetFilmsByStudioId(id);
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

        [HttpGet("sort/popularity")]
        public async Task<IActionResult> GetSortedFilmsByPopularity()
        {
            ViewData["Genres"] = await subservice.GetUsefullGenres();
            var films = await service.GetSortedFilmsByPopularity();
            if (films == null)
                return NoContent();
            ViewData["TypeOperation"] = "popularity";
            return View("SearchSorted", films);
            //return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("sort/rating")]
        public async Task<IActionResult> GetSortedFilmsByRating()
        {
            ViewData["Genres"] = await subservice.GetUsefullGenres();
            var films = await service.GetSortedFilmsByRating();
            if (films == null)
                return NoContent();
            ViewData["TypeOperation"] = "rating";
            return View("SearchSorted", films);
            //return Ok(JsonConvert.SerializeObject(films));
        }

        [HttpGet("sort/release")]
        public async Task<IActionResult> GetSortedFilmsByRelease()
        {
            ViewData["Genres"] = await subservice.GetUsefullGenres();
            var films = await service.GetSortedFilmsByRelease();
            if (films == null)
                return NoContent();
            ViewData["TypeOperation"] = "release";
            return View("SearchSorted", films);
            //return Ok(JsonConvert.SerializeObject(films));
        }
    }
}
