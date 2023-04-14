using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Application.Interaces;
using School.Domain.DTOs.News;
using School.Domain.DTOs.Paging;

namespace School.WebApi.Areas.Admin.Controllers
{
    [Route("api/News")]
    
    public class NewsController : AdminBaseController
    {
        #region constructor
        private readonly ISiteService _siteService;

        public NewsController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        #endregion

        #region filter-news
        [HttpGet("filter")]
        public async Task<IActionResult> FilterNews([FromQuery] RequestParams filter)
        {
            var results = await _siteService.FilterNewses(filter);
            return Ok(results);
        }

        #endregion

        #region create-news



        [HttpPost]
        public async Task<IActionResult> CreateNews([FromForm] CreateNewsDTO create)
        {

            var result = await _siteService.CreateNews(create);
            switch (result)
            {
                case CreateNewsResult.UrlExist:
                    return NotFound("لطفا برای خبر یک آدرسUrl دیگر انتخاب کنید");

                case CreateNewsResult.NotImage:
                    return NotFound("لطفا برای خبر یک تصویر انتخاب کنید");
                case CreateNewsResult.Success:
                    return Ok("عملیات ثبت خبر با موفقیت انجام شد");
            }

            return BadRequest();
        }
        #endregion

        #region edit-news
        [HttpGet("{newsId}")]
        public async Task<IActionResult> EditNews(int newsId)
        {
            var editNews = await _siteService.GetEditNews(newsId);
            if (editNews == null) return NotFound();
            return Ok(editNews);
        }

        [HttpPut("{newsId}")]
        public async Task<IActionResult> EditCollege([FromForm] EditNewsDTO  editNews, int newsId)
        {

            var result = await _siteService.EditNews(editNews, newsId);

            switch (result)
            {
                case EditNewsResult.UrlExist:
                    return NotFound("لطفا برای خبر یک آدرسUrl دیگر انتخاب کنید");

                case EditNewsResult.NotFound:

                    return NotFound(" یافت نشد");

                case EditNewsResult.Success:

                    return Ok(" با موفقیت ویرایش شد");

            }
            return BadRequest();
        }

        #endregion

        #region remove-news

        [HttpDelete("newsId")]
        public async Task<IActionResult> DeleteCollage(int newsId)
        {
            var result = await _siteService.DeleteNews(newsId);
            if (result == false) return NotFound();
            return Ok("خبر با موفقیت پاک شد");
        }


        #endregion



        #region news-galleries
        #region create
        [HttpPost("{newsId}")]
        public async Task<IActionResult> AddImageToNews(int newsId, [FromForm] List<IFormFile> images)
        {
            var result = await _siteService.AddNewsGallery(newsId, images);
            if (result)
            {
                return Ok("با موفقیت اضافه شد");
            }
            return NotFound("یافت نشد");
        }

        #endregion
        #region edit
        [HttpGet("gallery/{newsId}")]
        public async Task<IActionResult> NewsGalleries(int newsId)
        {
            var data = await _siteService.GetAllNewsGallries(newsId);
            return Ok(data);
        }
        #endregion
        #region delete

        [HttpDelete("deleteimage/{galleryId}")]
        public async Task<IActionResult> DeleteImage(int galleryId)
        {
            var result = await _siteService.DeleteNewsGalleryImage(galleryId);
            if (result == false) return NotFound("یافت نشد");
            return Ok("با موفقیت پاک شد");
        }
        #endregion
        #endregion
    }
}
