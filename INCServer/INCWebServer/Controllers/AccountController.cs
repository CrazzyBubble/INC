using INCServer;
using INCWebServer.Models;
using INCWebServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INCWebServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly LoginService service;
        public AccountController(LoginService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetView()
        {
            return View("Main");
        }
        [HttpPost("token")]
        public IActionResult Token(LoginModel model)
        {
            var identity = GetIdentity(model);
            if (identity == null)
            {
                return BadRequest("Invalid username or password.");
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(JsonConvert.SerializeObject(response));
        }

        private ClaimsIdentity GetIdentity(LoginModel model)
        {
            User person = service.SignIn(model).Result;
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Right.Name)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims, 
                    "Token", 
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
        /*private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> SignIn(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = service.SignIn(model).Result;
                if (user != null)
                {
                    await Authenticate(model.Email); // аутентификация
                    return Ok(); //RedirectToAction("", "search");
                }
                ModelState.AddModelError("", "Incorrect login or password");
            }
            return NotFound();
        }

        [HttpPost("registration")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<string>> SignUp(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var isOk = service.Registration(model);
                if (isOk)
                {
                    await Authenticate(model.Email);
                    return Ok();//RedirectToAction("", "search");
                }
                else
                    ModelState.AddModelError("", $"User with this email already exist");
            }
            return BadRequest();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }*/
    }
}
