using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Application.Interaces;
using School.Domain.Validator;
using School.Domain.ViewModels;

namespace School.WebApi.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region constructor
        private readonly IStudentServices _studentServices;
        private readonly ICaptchaValidator _captchaValidator;

        public AccountController(IStudentServices studentServices, ICaptchaValidator captchaValidator)
        {
            _studentServices = studentServices;
            _captchaValidator = captchaValidator;
        }
        #endregion


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterStudentDto register)
        {

            //#region captcha Validator
            //if (!await _captchaValidator.IsCaptchaPassedAsync(register.Token))
            //{
            //    return BadRequest("کد کپچای شما معتبر نمیباشد");
            //}
            //#endregion

            var validator = new RegisterStudnetValidator();
            var validateResult = await validator.ValidateAsync(register);


            if (!validateResult.IsValid)
            {
                return BadRequest(validateResult.Errors[0].ErrorMessage);
            }


            var result = await _studentServices.RegisterStudent(register);
            switch (result)
            {
                case RegisterStudentResult.PhoneNumberExists:
                    return BadRequest("شماره موبایل قبلا در سایت موجود است");

                case RegisterStudentResult.NatonaiCodeExists:
                    return BadRequest("کد ملی قبلا در سایت موجود است");

                case RegisterStudentResult.Success:
                    return Ok();

            }
            return NotFound();
        }
    }
}
