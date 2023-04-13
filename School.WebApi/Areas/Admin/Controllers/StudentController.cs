using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Application.Interaces;
using School.Domain.DTOs.Admin.Account;

namespace School.WebApi.Areas.Admin.Controllers
{
   
    [Route("api/Student")]
   
    public class StudentController : AdminBaseController
    {
        #region constructor
        private readonly IStudentServices _studentServices;

        public StudentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }
        #endregion


        #region filter-user
        [HttpGet]
        public async Task<IActionResult> FilterStudents([FromQuery] FilterStudentDTO filter)
        {
           var results = await _studentServices.FilterStudnets(filter);
            return Ok(results);
        }
        #endregion

        #region edit-user
        [HttpGet("{studentId}")]
        public async Task<IActionResult> EditUser(int studentId)
        {
            var student = await _studentServices.GetEditStudentFromAdmin(studentId);
            if (student == null) return NotFound();

            return Ok(student);
        }
        [HttpPut]
        public async Task<IActionResult> EditUser(EditUserFromAdminDTO editStudent)
        {
            var result = await _studentServices.EditStudentFromAdmin(editStudent);
            switch (result)
            {

                case EditStudentFromAdminResult.NotFound:
                    return NotFound("کاربری یافت نشد");

                case EditStudentFromAdminResult.Success:

                    return Ok("تغییرات با موفقیت انجام شد");
            }

            return BadRequest();
        }

        #endregion

        #region delete-user
        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent( int studentId)
        {
            var result = await _studentServices.DeleteStudent(studentId);
            if (result)
            {
                return Ok(" با موفقیت حذف شد ");
            }
            
            return NotFound(" یافت نشد");
        }
        #endregion

        #region recover-user

        [HttpGet("recover/{studentId}")]
        public async Task<IActionResult> RecoverStudent( int studentId)
        {
            var result = await _studentServices.RecoverStudent(studentId);
            if (result)
            {
                return Ok(" با موفقیت بازگردانی شد ");
            }
            
            return NotFound(" یافت نشد");
        }

        #endregion

        #region change-student
        [HttpGet("change-student/{studentId}")]
        public async Task<IActionResult> ChangeToStudent(int studentId)
        {

            var result = await _studentServices.ChangeToStudent(studentId);
            if (result)
            {
                return Ok(" با موفقیت انجام شد ");
            }

            return NotFound(" یافت نشد");
        }
        [HttpGet("change-substudent/{studentId}")]
        public async Task<IActionResult> ChangeToSubStudent(int studentId)
        {

            var result = await _studentServices.ChangeToSubStudent(studentId);
            if (result)
            {
                return Ok(" با موفقیت انجام شد ");
            }

            return NotFound(" یافت نشد");
        }

        #endregion
    }
}
