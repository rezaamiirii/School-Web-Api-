using Microsoft.AspNetCore.Http;
using School.Domain.DTOs.Collage;
using School.Domain.DTOs.Paging;
using School.Domain.Models.College;
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

        #region Academy

        #endregion
        #region College
        Task<IList<FilteringCollegeDTO>> FilterColleges(RequestParams request );

        Task<CreateCollegeResult> CreateCollege(CreateCollegeDTO createProduct);

        Task<EditCollegeDTO> GetEditCollege(int collegeId);

        Task<EditCollegeResult> EditCollege(EditCollegeDTO editCategory,int collegeId);
        Task<IReadOnlyList<College>> GetAllColleges();
        Task<bool> DeleteCollege(int collegeId);
        #endregion
        #region Athletic

        #endregion
    }
}
