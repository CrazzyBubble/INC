using INCWebServer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INCWebServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        LoginPageService service;
        public AccountController(LoginPageService service)
        {
            this.service = service;
        }

        [HttpGet("login")]
        public ActionResult<string> LogIn(string login, string password)
        {
            var user = service.SignIn(login, password).Result;
            if (user is null)
                return NotFound("Incorrect login or password");
            return Accepted(JsonConvert.SerializeObject(user));
        }

        [HttpPut("registration")]
        public ActionResult<string> LogUp(string email, string password, string firstname, DateTime birthday, string lastname="")
        {
            var isOk = service.Registration(email, password, firstname, lastname, birthday);
            if (isOk)
                return Accepted();
            else
                return BadRequest($"User with this email ({email}) already exist");
        }
    }
}
