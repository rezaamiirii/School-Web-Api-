using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Application.Interaces;
using School.Domain.DTOs.Student;
using School.WebApi.Extentions;
using System.Diagnostics;
using System.Security.Claims;
using ZarinpalSandbox;

namespace School.WebApi.Areas.Student.Controllers
{
    [Authorize]
    [Area("Student")]
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        #region constructor

        private readonly IStudentServices _studentServices;
        private readonly IConfiguration _configuration;

        public AccountController(IStudentServices studentServices, IConfiguration configuration)
        {
            _studentServices = studentServices;
            _configuration = configuration;
        }


        #endregion


        #region edit-user-profile

        [HttpGet()]
        public async Task<IActionResult> EditUserProfile()
        {

            var student = await _studentServices.GetEditStudentProfile(User.GetUserId());
            if (student == null) return NotFound("کاربری یافت نشد");

            return Ok(student);
        }


        [HttpPut()]
        public async Task<IActionResult> EditUserProfile(EditStudentProfileDTO editUserProfile)
        {
          
                var result = await _studentServices.EditProfile(User.GetUserId(), editUserProfile);
               
                switch (result)
                {

                    case EditStudentProfileResult.NotFound:
                        return NotFound("کاربری با مشخصات وارد شده یافت نشد");
                    case EditStudentProfileResult.Success:
                        return Ok("اطلاعات با موفقیت ویرایش شد");

                }
            
            return BadRequest();

            
        }
        #endregion

       
    }
}
