using Shop.Domain.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Models.College
{
    public class College: BaseEntity
    {
        #region properties
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? ImageName { get; set; }
        #endregion

        #region relations

        public ICollection<CollegeGallery>  CollegeGalleries { get; set; }
        #endregion
    }
}
