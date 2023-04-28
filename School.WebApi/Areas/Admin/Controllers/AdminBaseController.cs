using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.WebApi.Extentions;

namespace School.WebApi.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    [ApiController]
    
    public class AdminBaseController : ControllerBase
    {
    }
}
