using School.Domain.DTOs.Paging;
using School.Domain.Models.College;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace School.Domain.Interaces
{
    public interface ICollegeRepository:IGenericRepository<College>
    {
        Task<IPagedList<College>> FilterColleges(RequestParams request);

    }
}
