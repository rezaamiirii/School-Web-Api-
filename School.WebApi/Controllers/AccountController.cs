using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using School.Application.Interaces;
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

        public AccountController(IStudentServices studentServices, ICaptchaValidator captchaValidator, IConfiguration configuration)
        {
            _studentServices = studentServices;
            _captchaValidator = captchaValidator;
            _configuration = configuration;
        }
        #endregion

        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterStudentDto register)
        {

            //#region captcha Validator
            //if (!await _captchaValidator.IsCaptchaPassedAsync(register.Token))
            //{
            //    return BadRequest("کد کپچای شما معتبر نمیباشد");
            //}
            //#endregion

           


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
            return BadRequest();
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginStudentDTO login)
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
                    #region madaeni
                    //    var securityKey = new SymmetricSecurityKey(
                    //Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
                    //    var signingCredentials = new SigningCredentials(
                    //securityKey, SecurityAlgorithms.HmacSha256);

                    //    var Student = await _studentServices.GetStudentByPhoneNumber(login.StudentPhoneNumber);

                    //    var claimsForToken = new List<Claim>();
                    //    claimsForToken.Add(new Claim(ClaimTypes.NameIdentifier, Student.Id.ToString()));
                    //    claimsForToken.Add(new Claim(ClaimTypes.Name, Student.StudentPhoneNumber));

                    //    var jwtSecurityToke = new JwtSecurityToken(
                    //_configuration["Authentication:Issuer"],
                    //_configuration["Authentication:Audience"],
                    //claimsForToken,
                    //DateTime.Now,
                    //DateTime.Now.AddHours(1),
                    //signingCredentials
                    //);

                    //    var tokenToReturn = new JwtSecurityTokenHandler()
                    //        .WriteToken(jwtSecurityToke);
                    //    return Ok(tokenToReturn);

                    #endregion
                    #region devtube
                    //var student =await _studentServices.GetStudentByPhoneNumber(login.StudentPhoneNumber);
                    //var token = GenerateNewToken(student.Id,student.StudentPhoneNumber);


                    //var info = new AuthenticateViewModel
                    //{
                    //    FullName = $"{user.FirstName} {user.LastName}",
                    //    UserId = user.Id,
                    //    Token = token,
                    //    RefreshToken = refreshToken.ToString()
                    //};

                    //var userRefreshToken = await _userService.GetRefreshTokenAsync(user.Id);
                    //var userToken = new UserRefreshTokenViewModel
                    //{
                    //    UserId = user.Id,
                    //    RefreshToken = refreshToken.ToString(),
                    //    GenerateDate = DateTime.Now,
                    //    IsValid = true
                    //};

                    //if (userRefreshToken == null)
                    //{
                    //    //insert new refresh token in table
                    //    await _userService.InsertRefreshTokenAsync(userToken);
                    //}
                    //else
                    //{
                    //    //update record in db
                    //    await _userService.UpdateRefreshTokenAsync(userToken);
                    //}

                    //return Ok(token);
                    #endregion
                    #region mizfa
                    return Ok( await GenerateNewToken(login.StudentPhoneNumber));
                    #endregion
            }
            return NotFound();
        }



        private async Task<string> GenerateNewToken(string studentNumber)
        {
            #region devtube
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenKey"));
            //var tokenTimeOut = _configuration.GetValue<int>("TokenTimeOut");

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, studentNumber),
            //        new Claim(ClaimTypes.NameIdentifier, studen.ToString())
            //    }),

            //    Expires = DateTime.UtcNow.AddMinutes(tokenTimeOut),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);
            #endregion
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
        #region log-out

        [HttpGet("log-Out")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
        #endregion


        #region activate-account

        [HttpGet("active-account/{mobile}")]
        public async Task<IActionResult> ActiveAccount(string mobile)
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            var activeaccount = new ActiveAccountDTO()
            {
                PhoneNumber = mobile,
            };
            return Ok(activeaccount);
        }
        [HttpPost("active-account/{mobile}")]
        public async Task<IActionResult> ActiveAccount(ActiveAccountDTO active)
        {
            #region captcha Validator
            //if (!await _captchaValidator.IsCaptchaPassedAsync(active.Token))
            //{
            //    TempData[ErrorMessage] = "کد کپچای شما معتبر نمیباشد";
            //    return View(active);
            //}
            #endregion

            
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

      
        [HttpPost("forget-pass"), ]
        public async Task<IActionResult> ForgetPass(ForgetPasswordDTO forgetPassword)
        {
            #region captcha Validator
            //if (!await _captchaValidator.IsCaptchaPassedAsync(forgetPassword.Token))
            //{
            //    TempData[ErrorMessage] = "کد کپچای شما معتبر نمیباشد";
            //    return View(forgetPassword);
            //}
            #endregion
          
                var result = await _studentServices.ForgetPass(forgetPassword);
                switch (result)
                {
                    case forgetPassResult.NotFound:
                       return NotFound("شماره تلفن وارد شده معتبر نیست");
                    case forgetPassResult.Success:
                        return Ok( forgetPassword.PhoneNumber );
                }
            
            return NotFound(forgetPassword);
        }


        #endregion
        #region reset-pass
        [HttpGet("reset-forget-pass/{mobileActiveCode}")]
        public async Task<IActionResult> ResetPassBFromForgetPassword(string mobileActiveCode)
        {
            var student = await _studentServices.GetStudentByMobileActiveCode(mobileActiveCode);

            if (student == null) return NotFound();

            return Ok();
        }
        [HttpPost("reset-forget-pass/{mobileActiveCode}")]
        public async Task<IActionResult> ResetPassBFromForgetPassword(string mobileActiveCode,[FromBody] ResetPasswordDTO resetPassword)
        {
            #region captcha Validator
            //if (!await _captchaValidator.IsCaptchaPassedAsync(resetPassword.Token))
            //{
            //    TempData[ErrorMessage] = "کد کپچای شما معتبر نمیباشد";
            //    return View(resetPassword);
            //}
            #endregion
           
                var result = await _studentServices.ResettPasswordFromForget(resetPassword, mobileActiveCode);

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
