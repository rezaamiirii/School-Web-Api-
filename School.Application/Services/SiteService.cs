﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using School.Application.Extentions;
using School.Application.Interaces;
using School.Application.Utils;
using School.Domain.DTOs.Collage;
using School.Domain.DTOs.Paging;
using School.Domain.Interaces;
using School.Domain.Models.College;
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

        private readonly IMapper _mapper;

        public SiteService(IMapper mapper, ICollegeRepository collegeRepository, ICollegeGalleryRepository collegeGalleryRepository)
        {
            _mapper = mapper;
            _collegeRepository = collegeRepository;
            _collegeGalleryRepository = collegeGalleryRepository;
        }





        #endregion

        #region properties

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



        #endregion
    }
}
