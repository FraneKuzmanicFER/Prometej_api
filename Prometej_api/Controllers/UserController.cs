using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Prometej_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from User Controller");
        }
    }
}
