using Microsoft.EntityFrameworkCore;
using School.Domain.DTOs.Paging;
using School.Domain.Interaces;
using School.Domain.Models.College;
using Shop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace School.Infra.Data.Repositories
{
    public class CollegeRepository:GenericRepository<College>,ICollegeRepository
    {
        #region constructor
        private readonly ApplicationDbContext _context;

        public CollegeRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }


        #endregion

        #region properties

        public async Task<IPagedList<College>> FilterColleges(RequestParams request)
        {
            var query = _context.Colleges.AsQueryable();
            return await query.AsNoTracking().ToPagedListAsync(request.PageNumber, request.PageSize);


        }


        #endregion
    }
}
