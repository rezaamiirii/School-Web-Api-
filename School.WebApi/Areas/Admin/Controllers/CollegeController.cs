using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Application.Interaces;
using School.Domain.DTOs.Collage;
using School.Domain.DTOs.Paging;

namespace School.WebApi.Areas.Admin.Controllers
{

    [Authorize]
    [Area("Admin")]
    [Route("api/College")]
    [ApiController]
    public class CollegeController : ControllerBase
    {


        #region constructor
        private readonly ISiteService _siteService;

        public CollegeController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        #endregion

        #region filter-college
        [HttpGet("filter")]
        public async Task<IActionResult> FilterStudents([FromQuery] RequestParams filter)
        {
            var results = await _siteService.FilterColleges(filter);
            return Ok(results);
        }

        #endregion

        #region create-college



        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateCollegeDTO create)
        {

            var result = await _siteService.CreateCollege(create);
            switch (result)
            {
                case CreateCollegeResult.NotImage:
                    return NotFound("لطفا برای محصول یک تصویر انتخاب کنید");
                case CreateCollegeResult.Success:
                    return Ok("عملیات ثبت محصول با موفقیت انجام شد");
            }

            return BadRequest();
        }
        #endregion

        #region edit-college
        [HttpGet("{collegeId}")]
        public async Task<IActionResult> EditProduct(int collegeId)
        {
            var editCollege = await _siteService.GetEditCollege(collegeId);
            if (editCollege == null) return NotFound();
            return Ok(editCollege);
        }

        [HttpPut("{collegeId}")]
        public async Task<IActionResult> EditProduct([FromForm]EditCollegeDTO editCollege,int collegeId)
        {
            
                var result = await _siteService.EditCollege(editCollege,collegeId);

                switch (result)
                {

                    case EditCollegeResult.NotFound:
                        
                        return NotFound("محصولی یافت نشد");

                    case EditCollegeResult.Success:
                        
                        return Ok("محصول با موفقیت ویرایش شد");

                }
            return BadRequest();
        }

        #endregion

        #region remove-college

        [HttpDelete("collegeId")]
        public async Task<IActionResult> DeleteCollage(int collegeId)
        {
            var result = await _siteService.DeleteCollege(collegeId);
            if(result==false) return NotFound();
            return Ok("دانشکده با موفقیت پاک شد");
        }
        #endregion

        #region college-galleries
        #region create
        [HttpPost("{collegeId}")]
        public async Task<IActionResult> AddImageToCollege(int collegeId,[FromForm] List<IFormFile> images)
        {
            var result = await _siteService.AddCollegeGallery(collegeId, images);
            if (result)
            {
                return Ok("با موفقیت اضافه شد");
            }
            return NotFound("یافت نشد");
        }

        #endregion
        #region edit
        [HttpGet("gallery/{collegeId}")]
        public async Task<IActionResult> CollegeGalleries(int collegeId)
        {
            var data = await _siteService.GetAllCollegeGallries(collegeId);
            return Ok(data);
        }
        #endregion
        #region delete

        [HttpDelete("deleteimage/{galleryId}")]
        public async Task<IActionResult> DeleteImage(int galleryId)
        {
            var result=  await _siteService.DeleteCollegeGalleryImage(galleryId);
            if(result==false) return NotFound("یافت نشد");
            return Ok("با موفقیت پاک شد");
        }
        #endregion
        #endregion

    }
}
