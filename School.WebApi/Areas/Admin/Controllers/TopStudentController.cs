using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Application.Interaces;
using School.Domain.DTOs.Paging;
using School.Domain.DTOs.TopStudent;

namespace School.WebApi.Areas.Admin.Controllers
{
   
    [Route("api/TopStudent")]
    
    public class TopStudentController : AdminBaseController
    {
        #region constructor
        private readonly ISiteService _siteService;

        public TopStudentController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        #endregion

        #region filter-topstudent
        [HttpGet("filter")]
        public async Task<IActionResult> FilterTopStudents([FromQuery] RequestParams filter)
        {
            var results = await _siteService.FilterTopStudents(filter);
            return Ok(results);
        }

        #endregion

        #region create-topstudent



        [HttpPost]
        public async Task<IActionResult> CreateTopStudent([FromForm] CreateTopStudentDTO create)
        {

            var result = await _siteService.CreateTopStudent(create);
            switch (result)
            {
                case CreateTopStudentResult.NotImage:
                    return NotFound("لطفا برای فیلد یک تصویر انتخاب کنید");
                case CreateTopStudentResult.Success:
                    return Ok("عملیات ثبت  با موفقیت انجام شد");
            }

            return BadRequest();
        }
        #endregion


        #region edit-topstudent
        [HttpGet("{topStudentId}")]

        public async Task<IActionResult> EditTopStudent(int topStudentId)
        {
            var editTopStudent = await _siteService.GetEditTopStudent(topStudentId);
            if (editTopStudent == null) return NotFound();
            return Ok(editTopStudent);
        }

        [HttpPut("{topStudentId}")]
        public async Task<IActionResult> EditTopStudent([FromForm] EditTopStudentDTO  editTopStudent, int topStudentId)
        {

            var result = await _siteService.EditTopStudent(editTopStudent, topStudentId);

            switch (result)
            {

                case EditTopStudentResult.NotFound:

                    return NotFound(" یافت نشد");

                case EditTopStudentResult.Success:

                    return Ok(" با موفقیت ویرایش شد");

            }
            return BadRequest();
        }

        #endregion
       
        #region remove-topstudent

        [HttpDelete("topStudentId")]
        public async Task<IActionResult> DeleteTopStudent(int topStudentId)
        {
            var result = await _siteService.DeleteTopStudent(topStudentId);
            if (result == false) return NotFound();
            return Ok(" با موفقیت پاک شد");
        }
        #endregion

    }
}
