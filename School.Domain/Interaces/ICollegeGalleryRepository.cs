using School.Domain.Models.College;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Interaces
{
    public interface ICollegeGalleryRepository 
    {
        Task AddCollegeGalleries(List<CollegeGallery>  collegeGalleries);
        Task<IList<CollegeGallery>> GetAllCollegeGalleries(int collegeId);

        Task<CollegeGallery> GetCollegeGalleryImageById(int galleryId);
        Task DeleteCollegeGalleryImageById(int id);
        Task<bool> CheckCollege(int collegeId);
    }
}
