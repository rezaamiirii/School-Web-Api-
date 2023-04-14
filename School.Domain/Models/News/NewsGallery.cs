using Shop.Domain.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Models.News
{
    public class NewsGallery: BaseEntity
    {
        #region properties
        public int NewsId { get; set; }
        public string? ImageName { get; set; }

        #endregion

        #region relations
        public News?  News { get; set; }

        #endregion
    }
}
