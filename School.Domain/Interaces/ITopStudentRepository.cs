using School.Domain.DTOs.Paging;
using School.Domain.Models.TopStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace School.Domain.Interaces
{
    public interface ITopStudentRepository : IGenericRepository<TopStudent>
    {
        Task<IPagedList<TopStudent>> FilterTopStudent(RequestParams request);
    }
}
