using AutoMapper;
using Microsoft.AspNetCore.Http;
using School.Application.Extentions;
using School.Application.Interaces;
using School.Application.Utils;
using School.Domain.DTOs.Admin.Account;
using School.Domain.DTOs.Collage;
using School.Domain.DTOs.News;
using School.Domain.DTOs.Paging;
using School.Domain.DTOs.Student;
using School.Domain.DTOs.TopStudent;
using School.Domain.Interaces;
using School.Domain.Models.College;
using School.Domain.Models.News;
using School.Domain.Models.TopStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Services
{
    public class SiteService : ISiteService
    {
        #region constructore
        private readonly ICollegeRepository _collegeRepository;
        private readonly ICollegeGalleryRepository _collegeGalleryRepository;
        private readonly ITopStudentRepository  _topStudentRepository;
        private readonly INewsRepository  _newsRepository;
        private readonly IAdminRepository   _adminRepository;

        private readonly IMapper _mapper;

        public SiteService(IMapper mapper, ICollegeRepository collegeRepository, ICollegeGalleryRepository collegeGalleryRepository, ITopStudentRepository topStudentRepository, INewsRepository newsRepository, IAdminRepository adminRepository)
        {
            _mapper = mapper;
            _collegeRepository = collegeRepository;
            _collegeGalleryRepository = collegeGalleryRepository;
            _topStudentRepository = topStudentRepository;
            _newsRepository = newsRepository;
            _adminRepository = adminRepository;
        }





        #endregion

        #region properties

        #region login-admin
        public async Task<LoginAdminResult> LoginAdmin(LoginAdminDTO login)
        {
            var admin = await _adminRepository.GetadminByPhoneNumber(login.AdminPhoneNumber);

            if (admin.IsAdmin)
            {
                        if (login.Password == admin.Password)
                        {
                            return LoginAdminResult.Success;
                        }
                        return LoginAdminResult.NotFound;
                   
            }
            return LoginAdminResult.NotFound;
        }

        public async Task<Domain.Models.Account.Admin> GetAdminByPhoneNumber(string phoneNumber)
        {
           return await _adminRepository.GetadminByPhoneNumber(phoneNumber);
        }




        #endregion

        #region news
        public async Task<CreateNewsResult> CreateNews(CreateNewsDTO createNews)
        {
            if (await _newsRepository.CheckUrlNameNews(createNews.UrlName)) return CreateNewsResult.UrlExist;


            var newNews = new News();

            var result = _mapper.Map(createNews, newNews);


            if (createNews.Image != null && createNews.Image.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path
                    .GetExtension(createNews.Image.FileName);
                createNews.Image.AddImageToServer(imageName,
                    PathExtentions.NewsOrginServer
                    , 215, 215,
                    PathExtentions.NewsThumbServer);
                result.ImageName = imageName;
            }
            else
            {
                return CreateNewsResult.NotImage;
            }
            await _newsRepository.Add(result);

            await _newsRepository.SaveChanges();

            return CreateNewsResult.Success;

        }

        public async Task<EditNewsDTO> GetEditNews(int newsId)
        {
            var news = await _newsRepository.Get(newsId);
            if (news != null)
            {
                var editNews = new EditNewsDTO();
                editNews.Title = news.Title;
                editNews.ShortDescription = news.ShortDescription;
                editNews.Description = news.Description;
                editNews.ImageName = news.ImageName;
                editNews.UrlName = news.UrlName;
                return editNews;
            }
            return null;

        }

        public async Task<EditNewsResult> EditNews(EditNewsDTO editNews, int newsId)
        {
            var news = await _newsRepository.Get(newsId);

            if (news == null) return EditNewsResult.NotFound;
            if (await _newsRepository.CheckUrlNameNews(editNews.UrlName)) return EditNewsResult.UrlExist;

            news.ShortDescription = editNews.ShortDescription;
            news.Description = editNews.Description;
            news.Title = editNews.Title;
            news.UrlName = editNews.UrlName;


            if (editNews.Image != null && editNews.Image.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(editNews.Image.FileName);
                editNews.Image.AddImageToServer(imageName, PathExtentions.NewsOrginServer, 150, 150, PathExtentions.NewsThumbServer, news.ImageName);

                news.ImageName = imageName;
            }
            _newsRepository.Update(news);
            await _newsRepository.SaveChanges();

            return EditNewsResult.Success;


        }

        public async Task<IReadOnlyList<News>> GetAllNewses()
        {
            return await _newsRepository.GetAll();
        }

        public Task<bool> DeleteNews(int newsId)
        {
            return _newsRepository.DeleteNews(newsId);
        }

        public async Task<IList<FilteringNewsDTO>> FilterNewses(RequestParams request)
        {
            var newses = await _newsRepository.FilterNews(request);
            var result = _mapper.Map<IList<FilteringNewsDTO>>(newses);
            return result;

        }



        #endregion

        #region news-gallery
        public async Task<bool> AddNewsGallery(int newsId, List<IFormFile> images)
        {
            if (!await _newsRepository.CheckNews(newsId)) return false;
            if (images != null && images.Any())
            {
                var newsGallery = new List<NewsGallery>();
                foreach (var image in images)
                {
                    if (image.IsImage())
                    {
                        var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                        image.AddImageToServer(imageName, PathExtentions.NewsOrginServer, 150, 150, PathExtentions.NewsThumbServer);

                        newsGallery.Add(new NewsGallery
                        {
                            ImageName = imageName,
                            NewsId = newsId,
                        });

                    }
                }
                await _newsRepository.AddNewsGalleries(newsGallery);
                return true;
            }
            return false;


        }

        public async Task<IList<NewsGallery>> GetAllNewsGallries(int newsId)
        {
            return await _newsRepository.GetAllNewsGalleries(newsId);
        }

        public async Task<bool> DeleteNewsGalleryImage(int galleryId)
        {
            var newsgallery = await _newsRepository.GetNewsGalleryImageById(galleryId);
            if (newsgallery != null)
            {
                UploadImageExtension.DeleteImage(newsgallery.ImageName, PathExtentions.NewsOrginServer, PathExtentions.NewsThumbServer);
                await _newsRepository.DeleteNewsGalleryImageById(galleryId);
                return true;
            }

            return false;

        }


        #endregion

        #region college
        public async Task<CreateCollegeResult> CreateCollege(CreateCollegeDTO createProduct)
        {

            var newCollege = new College();

            var result = _mapper.Map(createProduct, newCollege);


            if (createProduct.Image != null && createProduct.Image.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path
                    .GetExtension(createProduct.Image.FileName);
                createProduct.Image.AddImageToServer(imageName,
                    PathExtentions.CollegeOrginServer
                    , 215, 215,
                    PathExtentions.CollegeThumbServer);
                result.ImageName = imageName;
            }
            else
            {
                return CreateCollegeResult.NotImage;
            }
            await _collegeRepository.Add(result);

            await _collegeRepository.SaveChanges();

            return CreateCollegeResult.Success;


        }

        public async Task<bool> DeleteCollege(int collegeId)
        {
            var college= await _collegeRepository.Get(collegeId);
            if(college == null) return false;
             await _collegeRepository.Delete(college);
            return true;
        }

        public async Task<EditCollegeResult> EditCollege(EditCollegeDTO editCategory, int collegeId)
        {
            var college = await _collegeRepository.Get(collegeId);

            if (college == null) return EditCollegeResult.NotFound;
            college.ShortDescription = editCategory.ShortDescription;
            college.Description = editCategory.Description;
            college.Title = editCategory.Title;


            if (editCategory.Image != null && editCategory.Image.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(editCategory.Image.FileName);
                editCategory.Image.AddImageToServer(imageName, PathExtentions.CollegeOrginServer, 150, 150, PathExtentions.CollegeThumbServer, college.ImageName);

                college.ImageName = imageName;
            }
            _collegeRepository.Update(college);
            await _collegeRepository.SaveChanges();

            return EditCollegeResult.Success;

        }

        public async Task<IList<FilteringCollegeDTO>> FilterColleges(RequestParams request)
        {
            var colleges= await _collegeRepository.FilterColleges(request);
            var result = _mapper.Map<IList<FilteringCollegeDTO>>(colleges);
            return result;
        }

        public async Task<IReadOnlyList<College>> GetAllColleges()
        {
            return await _collegeRepository.GetAll();
        }

        public async Task<EditCollegeDTO> GetEditCollege(int collegeId)
        {
            var college = await _collegeRepository.Get(collegeId);
            if (college != null)
            {
                var editcollege = new EditCollegeDTO();
                editcollege.Title = college.Title;
                editcollege.ShortDescription = college.ShortDescription;
                editcollege.Description = college.Description;
                editcollege.ImageName = college.ImageName;
                return editcollege;
            }
            return null;
        }
        #endregion

        #region college-gallery
        public async Task<bool> AddCollegeGallery(int collegeId, List<IFormFile> images)
        {
            if (!await _collegeGalleryRepository.CheckCollege(collegeId)) return false;
            if (images != null && images.Any())
            {
                var collegeGallery = new List<CollegeGallery>();
                foreach (var image in images)
                {
                    if (image.IsImage())
                    {
                        var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                        image.AddImageToServer(imageName, PathExtentions.CollegeOrginServer, 150, 150, PathExtentions.CollegeThumbServer);

                        collegeGallery.Add(new CollegeGallery
                        {
                            ImageName = imageName,
                            CollegeId = collegeId,
                        });

                    }
                }
                await _collegeGalleryRepository.AddCollegeGalleries(collegeGallery);
                return true;
            }
            return false;

        }

        public async Task<IList<CollegeGallery>> GetAllCollegeGallries(int collegeId)
        {
            return await _collegeGalleryRepository.GetAllCollegeGalleries(collegeId);
        }

        public async Task<bool> DeleteCollegeGalleryImage(int galleryId)
        {
            var collegetgallery = await _collegeGalleryRepository.GetCollegeGalleryImageById(galleryId);
            if (collegetgallery != null)
            {
                UploadImageExtension.DeleteImage(collegetgallery.ImageName, PathExtentions.CollegeOrginServer, PathExtentions.CollegeThumbServer);
                await _collegeGalleryRepository.DeleteCollegeGalleryImageById(galleryId);
                return true;
            }

            return false;
        }


        #endregion

        #region top-student
        public async Task<CreateTopStudentResult> CreateTopStudent(CreateTopStudentDTO createTopStudent)
        {
            var newTopStudent = new TopStudent();

            var result = _mapper.Map(createTopStudent, newTopStudent);


            if (createTopStudent.Image != null && createTopStudent.Image.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path
                    .GetExtension(createTopStudent.Image.FileName);
                createTopStudent.Image.AddImageToServer(imageName,
                    PathExtentions.TopStudentOrginServer
                    , 215, 215,
                    PathExtentions.TopStudentThumbServer);
                result.ImageName = imageName;
            }
            else
            {
                return CreateTopStudentResult.NotImage;
            }
            await _topStudentRepository.Add(result);

            await _collegeRepository.SaveChanges();

            return CreateTopStudentResult.Success;

        }

        public async Task<EditTopStudentDTO> GetEditTopStudent(int topStudentId)
        {
            var topStudent = await _topStudentRepository.Get(topStudentId);
            if (topStudent != null)
            {
                var editTopStudent = new EditTopStudentDTO();
                editTopStudent.Title = topStudent.Title;
                editTopStudent.ShortDescription = topStudent.ShortDescription;
                editTopStudent.Description = topStudent.Description;
                editTopStudent.ImageName = topStudent.ImageName;
                return editTopStudent;
            }
            return null;

        }

        public async Task<EditTopStudentResult> EditTopStudent(EditTopStudentDTO editTopStudent, int topStudentId)
        {
            var topStudent = await _topStudentRepository.Get(topStudentId);

            if (topStudent == null) return EditTopStudentResult.NotFound;
            topStudent.ShortDescription = editTopStudent.ShortDescription;
            topStudent.Description = editTopStudent.Description;
            topStudent.Title = editTopStudent.Title;


            if (editTopStudent.Image != null && editTopStudent.Image.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(editTopStudent.Image.FileName);
                editTopStudent.Image.AddImageToServer(imageName, PathExtentions.TopStudentOrginServer, 150, 150, PathExtentions.TopStudentThumbServer, topStudent.ImageName);

                topStudent.ImageName = imageName;
            }
            _topStudentRepository.Update(topStudent);
            await _collegeRepository.SaveChanges();

            return EditTopStudentResult.Success;


        }

        public async Task<IReadOnlyList<TopStudent>> GetAllTopStudents()
        {
            return await _topStudentRepository.GetAll();
        }

        public async Task<bool> DeleteTopStudent(int topStudentId)
        {
            var topStudent = await _topStudentRepository.Get(topStudentId);
            if (topStudent == null) return false;
            await _topStudentRepository.Delete(topStudent);
            return true;

        }

        public async Task<IList<FilteringTopStudentsDTO>> FilterTopStudents(RequestParams request)
        {
            var topStudents = await _topStudentRepository.FilterTopStudent(request);
            var result = _mapper.Map<IList<FilteringTopStudentsDTO>>(topStudents);
            return result;

        }

       
        #endregion


        #endregion
    }
}
