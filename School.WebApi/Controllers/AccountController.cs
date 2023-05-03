using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using School.Application.Interaces;
using School.Domain.DTOs.Admin.Account;
using School.Domain.DTOs.Student;
using School.Domain.DTOs.Student.Validator;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace School.WebApi.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region constructor
        private readonly IStudentServices _studentServices;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly IConfiguration _configuration;
        private readonly ISiteService _siteService;

        public AccountController(IStudentServices studentServices, ICaptchaValidator captchaValidator, IConfiguration configuration, ISiteService siteService)
        {
            _studentServices = studentServices;
            _captchaValidator = captchaValidator;
            _configuration = configuration;
            _siteService = siteService;
        }
        #endregion


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterStudentDto register)
        {


            var result = await _studentServices.RegisterStudent(register);
            switch (result)
            {
                case RegisterStudentResult.PhoneNumberExists:
                    return BadRequest("شماره موبایل قبلا در سایت موجود است");

                case RegisterStudentResult.NatonaiCodeExists:
                    return BadRequest("کد ملی قبلا در سایت موجود است");

                case RegisterStudentResult.Success:

                    return Ok(register.StudentPhoneNumber);

            }
            return BadRequest();
        }


        [HttpPost("loginAdmin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginAdminDTO loginAdmin)
        {
            var result = await _siteService.LoginAdmin(loginAdmin);

            switch (result)
            {
                case LoginAdminResult.NotFound:

                    return Unauthorized("کاربری یافت نشد");

                case LoginAdminResult.Success:


                    return Ok(await GenerateNewTokenForAdmin(loginAdmin.AdminPhoneNumber));

            }
            return NotFound();
        }

        [HttpPost("loginStudent")]
        public async Task<IActionResult> LoginStudent([FromBody] LoginDTO login)
        {

            var result = await _studentServices.LoginStudent(login);
            switch (result)
            {
                case LoginStudentResult.NotFound:

                    return Unauthorized("کاربری یافت نشد");

                case LoginStudentResult.NotActive:
                    return BadRequest("حساب کاربری شما فعال نمیباشد");

                case LoginStudentResult.IsBlocked:
                    return BadRequest("حساب شما توسط واحد پشتیبانی مسدود شده است");
                case LoginStudentResult.Success:

                   
                    #region mizfa
                    return Ok(await GenerateNewToken(login.StudentPhoneNumber));
                    #endregion
            }
            return NotFound();
        }


        #region student-claim
        private async Task<string> GenerateNewToken(string studentNumber)
        {
           
            var secretKey = Encoding.UTF8.GetBytes(_configuration["SiteSettings:SecretKey"]);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encrytionKey = Encoding.UTF8.GetBytes(_configuration["SiteSettings:EncrypKey"]);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encrytionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);


            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _configuration["SiteSettings:Issuer"],
                Audience = _configuration["SiteSettings:Audience"],
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(await GetClaimsAsync(studentNumber)),
                EncryptingCredentials = encryptingCredentials,

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
        private async Task<IEnumerable<Claim>> GetClaimsAsync(string studentPhoneNumber)
        {
           
            var Student = await _studentServices.GetStudentByPhoneNumber(studentPhoneNumber);
           

            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,Student.StudentPhoneNumber),
                new Claim(ClaimTypes.NameIdentifier,Student.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            return Claims;
        }
        #endregion

        #region admin-claim
        private async Task<string> GenerateNewTokenForAdmin(string adminNumber)
        {
           
            var secretKey = Encoding.UTF8.GetBytes(_configuration["SiteSettings:SecretKey"]);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encrytionKey = Encoding.UTF8.GetBytes(_configuration["SiteSettings:EncrypKey"]);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encrytionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);


            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _configuration["SiteSettings:Issuer"],
                Audience = _configuration["SiteSettings:Audience"],
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(await GetClaimsForAdminAsync(adminNumber)),
                EncryptingCredentials = encryptingCredentials,

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
        private async Task<IEnumerable<Claim>> GetClaimsForAdminAsync(string adminPhoneNumber)
        {
            var admin = await _siteService.GetAdminByPhoneNumber(adminPhoneNumber);

            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,admin.PhoneNumber),
                new Claim(ClaimTypes.NameIdentifier,admin.Id.ToString()),
                new Claim(ClaimTypes.Role,admin.Role),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            return Claims;
        }
        #endregion


        #region log-out

        [HttpGet("log-Out")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
        #endregion


        #region activate-account

      
        [HttpPost("active-account")]
        public async Task<IActionResult> ActiveAccount([FromBody] ActiveAccountDTO active)
        {
           

            var result = await _studentServices.ActiveAccount(active);
            switch (result)
            {
                case ActiveAccountResult.Error:

                    return BadRequest("فعال سازی حساب کاربری با شکست مواجه شد");
                case ActiveAccountResult.NotFound:

                    return NotFound("کاربری یافت نشد");
                case ActiveAccountResult.Success:

                    return Ok("فعال سازی حساب کاربری با موفقیت انجام شد");
            }

            return NotFound(active);
        }

        #endregion

        #region forget pass


        [HttpPost("forget-pass")]
        public async Task<IActionResult> ForgetPass(ForgetPasswordDTO forgetPassword)
        {
           

            var result = await _studentServices.ForgetPass(forgetPassword);
            switch (result)
            {
                case forgetPassResult.NotFound:
                    return NotFound("شماره تلفن وارد شده معتبر نیست");
                case forgetPassResult.Success:
                    return Ok(forgetPassword.PhoneNumber);
            }

            return NotFound(forgetPassword);
        }


        #endregion
        #region reset-pass
       
        [HttpPost("reset-forget-pass")]
        public async Task<IActionResult> ResetPassBFromForgetPassword( [FromBody] ResetPasswordDTO resetPassword)
        {
           

            var result = await _studentServices.ResettPasswordFromForget(resetPassword, resetPassword.MobileActiveCode);

            switch (result)
            {
                case resetPasswordResult.Error:

                    return NotFound("کاربری یافت نشد ");
                case resetPasswordResult.Success:

                    return Ok("عملیات تغییر رمز با موفقیت انجام شد");
            }



            return BadRequest();
        }
        #endregion
    }
}
