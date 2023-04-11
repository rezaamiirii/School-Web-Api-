using AutoMapper;
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

        private readonly IMapper _mapper;

        public SiteService(IMapper mapper, ICollegeRepository collegeRepository)
        {
            _mapper = mapper;
            _collegeRepository = collegeRepository;
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
                    PathExtentions.AcademyOrginServer
                    , 215, 215,
                    PathExtentions.AcademyThumbServer);
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
                editCategory.Image.AddImageToServer(imageName, PathExtentions.AcademyOrginServer, 150, 150, PathExtentions.AcademyThumbServer, college.ImageName);

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




        #endregion
    }
}
