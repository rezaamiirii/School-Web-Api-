using Microsoft.EntityFrameworkCore;
using School.Domain.Interaces;
using School.Domain.Models.College;
using Shop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infra.Data.Repositories
{
    public class CollegeGalleryRepository : ICollegeGalleryRepository
    {
        #region constructor
        private readonly ApplicationDbContext _context;

        public CollegeGalleryRepository(ApplicationDbContext context) 
        {
            _context = context;
        }




        #endregion

        #region properties
        public async Task AddCollegeGalleries(List<CollegeGallery> collegeGalleries)
        {
            await _context.CollegeGalleries.AddRangeAsync(collegeGalleries);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> CheckCollege(int collegeId)
        {
            return await _context.Colleges.AnyAsync(x=>x.Id==collegeId&&!x.IsDelete);
        }

        public async Task DeleteCollegeGalleryImageById(int id)
        {
            var cuurentproductgallery = await _context.CollegeGalleries.SingleOrDefaultAsync(i => i.Id == id);
            if (cuurentproductgallery != null)
            {
                cuurentproductgallery.IsDelete = true;
                _context.CollegeGalleries.Update(cuurentproductgallery);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IList<CollegeGallery>> GetAllCollegeGalleries(int collegeId)
        {
            var collegegalleries = await _context.CollegeGalleries
                .AsQueryable()
                .Where(p => p.CollegeId == collegeId && !p.IsDelete).ToListAsync();
            return collegegalleries;

        }

        public async Task<CollegeGallery> GetCollegeGalleryImageById(int galleryId)
        {
            var collegeGallery = await _context.CollegeGalleries.SingleOrDefaultAsync(i => i.Id == galleryId&&!i.IsDelete);
            if (collegeGallery != null) return collegeGallery;
            return null;

        }
        #endregion
    }
}
