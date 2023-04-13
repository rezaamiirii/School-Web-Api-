using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.WebApi.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [ApiController]
    public class AdminBaseController : ControllerBase
    {
    }
}
