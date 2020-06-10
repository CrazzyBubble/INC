using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INCWebServer.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INCWebServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        LoginPageService service;
        public LoginController(LoginPageService service)
        {
            this.service = service;
        }
    }
}
