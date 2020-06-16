using Microsoft.AspNetCore.Mvc;

namespace INCWebServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController: Controller
    {
        [HttpGet]
        public void GetPage()
        {

        }
    }
}
