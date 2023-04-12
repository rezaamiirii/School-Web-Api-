using Shop.Domain.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Models.College
{
    public class CollegeGallery:BaseEntity
    {
        #region properties
        public int CollegeId { get; set; }
        public string? ImageName { get; set; }

        #endregion

        #region relations
        public College? College { get; set; }

        #endregion
    }
}
