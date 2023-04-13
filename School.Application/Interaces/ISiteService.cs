using Microsoft.AspNetCore.Http;
using School.Domain.DTOs.Collage;
using School.Domain.DTOs.Paging;
using School.Domain.DTOs.TopStudent;
using School.Domain.Models.College;
using School.Domain.Models.TopStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Interaces
{
    public interface ISiteService
    {
        #region News

        #endregion

       
        #region College
        Task<IList<FilteringCollegeDTO>> FilterColleges(RequestParams request );

        Task<CreateCollegeResult> CreateCollege(CreateCollegeDTO createProduct);

        Task<EditCollegeDTO> GetEditCollege(int collegeId);

        Task<EditCollegeResult> EditCollege(EditCollegeDTO editCategory,int collegeId);
        Task<IReadOnlyList<College>> GetAllColleges();
        Task<bool> DeleteCollege(int collegeId);
        #endregion

        #region college-gallery
        Task<bool> AddCollegeGallery(int collegeId, List<IFormFile> images);

        Task<IList<CollegeGallery>> GetAllCollegeGallries(int collegeId);

        Task<bool> DeleteCollegeGalleryImage(int galleryId);

        #endregion
        #region top-student
        Task<IList<FilteringTopStudentsDTO>> FilterTopStudents(RequestParams request);

        Task<CreateTopStudentResult> CreateTopStudent(CreateTopStudentDTO  createTopStudent);

        Task<EditTopStudentDTO> GetEditTopStudent(int topStudentId);

        Task<EditTopStudentResult> EditTopStudent(EditTopStudentDTO  editTopStudent, int topStudentId);
        Task<IReadOnlyList<TopStudent>> GetAllTopStudents();
        Task<bool> DeleteTopStudent(int topStudentId);

        #endregion
    }
}
