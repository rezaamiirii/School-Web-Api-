using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace School.WebApi.Controllers
{
    [Route("api/home")]
    [Authorize]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        public IActionResult Index()
        {
            return Ok();
        }
        
    }
}
