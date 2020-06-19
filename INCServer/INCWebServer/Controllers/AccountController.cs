using INCServer;
using INCWebServer.Models;
using INCWebServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INCWebServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly AccountService service;
        private readonly SubsidiaryService subservice;
        public AccountController(AccountService service, SubsidiaryService subservice)
        {
            this.service = service;
            this.subservice = subservice;
        }

        [HttpGet]
        public IActionResult GetView()
        {
            return View("Main");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);
            var identity = await GetIdentity(model);
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

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!service.Registration(model))
                {
                    return BadRequest("Email already exist!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        private async Task<ClaimsIdentity> GetIdentity(LoginModel model)
        {
            User person = await service.SignIn(model);
            if (person == null)
                return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, subservice.GetPersonRight(person.Rightid)??"")
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, 
                "Token", 
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
