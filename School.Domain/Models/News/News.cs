using Shop.Domain.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Models.News
{
    public class News : BaseEntity
    {
        #region properties
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? UrlName { get; set; }
        public string? ImageName { get; set; }
        #endregion

        #region relations

        public ICollection<NewsGallery>  NewsGalleries { get; set; }
        #endregion
    }
}
