using INCWebServer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INCWebServer.Controllers
{
    [ApiController]
    [Route("[controller]/film")]
    public class ViewController : Controller
    {
        private readonly ViewService service;
        private readonly SubsidiaryService subservice;
        public ViewController(ViewService service, SubsidiaryService subservice)
        {
            this.service = service;
            this.subservice = subservice;
        }

        [HttpGet]
        public IActionResult GetFullFilm(int id=-1)
        {
            if (id == -1)
                return BadRequest();
            ViewData["Genres"] = subservice.GetUsefullGenres().Result;
            var film = service.GetFilmById(id).Result;
            if (film is null)
                return NotFound("Film not found");
            return View("Watch", film);
            //return Ok(JsonConvert.SerializeObject(film));
        }

        [HttpGet("last")]
        public IActionResult GetLastFilm(int userid)
        {
            ViewData["Genres"] = subservice.GetUsefullGenres().Result;
            var film = service.GetLastFilm(userid).Result;
            if (film is null)
                return NotFound("Film not found");
            return View("Watch", film);
            //return Ok(JsonConvert.SerializeObject(film));
        }

        [HttpPut("watch")]
        public ActionResult<string> SetNewWatched(int userid, int filmid)
        {
            var isset = service.SetWatched(userid, filmid);
            if (!isset)
                return BadRequest("Already exist");
            return Ok();
        }

        [HttpPost("iswatched")]
        public ActionResult<string> SetWatchTrue(int userid, int filmid)
        {
            var isset = service.SetWatchTrue(userid, filmid);
            if (!isset)
                return NotFound();
            return Ok();
        }

        [HttpPost("timestop")]
        public ActionResult<string> SetTimeStop(int userid, int filmid, TimeSpan timestop)
        {
            var isset = service.SetTimeStop(userid, filmid, timestop);
            if (!isset)
                return NotFound();
            return Ok();
        }

        [HttpPost("like")]
        public ActionResult<string> SetLike(int userid, int filmid, bool islike)
        {
            var isset = service.SetLike(userid, filmid, islike);
            if (!isset)
                return NotFound();
            return Ok();
        }

        [HttpPost("rating")]
        public ActionResult<string> SetRating(int userid, int filmid, short rating)
        {
            var isset = service.SetRating(userid, filmid, rating);
            if (!isset)
                return NotFound();
            return Ok();
        }
    }
}
