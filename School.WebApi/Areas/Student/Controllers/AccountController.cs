using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Application.Interaces;
using School.Domain.DTOs.Fee;
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

        #region payfee
       
        [HttpPost("PayFee")]
        public async Task<IActionResult> PayFee([FromBody] PayFeeDTO payFee)
        {
            var studentFeeId = await _studentServices.ChargeFee(User.GetUserId(), payFee,$"پرداخت شهریه به مبلغ {payFee.Amount}");

            #region payment

            var payment = new Payment(payFee.Amount);
            var url = _configuration.GetValue<string>("DefaultURL:Host") + "/user/online-payment/" + studentFeeId;
            var result = payment.PaymentRequest("شارژ کیف پول", url);

            if (result.Result.Status == 100)
            {
                return Ok("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
            }
           
            #endregion
            return BadRequest();
        }
        #endregion
        #region online-pament
        [HttpGet("online-payment/{id}")]
        public async Task<IActionResult> OnlinePayment(int id)
        {

            if (HttpContext.Request.Query["Status"] != ""
                && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
               && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];
                var studentFee = await _studentServices.GetStudentFeeById(id);
                if (studentFee != null)
                {
                    var payment = new Payment(studentFee.Amount);
                    var result = payment.Verification(authority).Result;

                    if (result.Status == 100)
                    {
                      
                        await _studentServices.UpdateStudentFeeForCharge(studentFee);
                        return Ok();
                    }
                    return NotFound();
                }
                return NotFound();
            }


            return BadRequest();
        }

        #endregion
    }
}
