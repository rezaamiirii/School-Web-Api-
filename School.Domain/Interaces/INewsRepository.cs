using School.Domain.DTOs.Paging;
using School.Domain.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace School.Domain.Interaces
{
    public interface INewsRepository:IGenericRepository<News>
    {
        #region news
        Task<IPagedList<News>> FilterNews(RequestParams request);
        Task<bool> CheckUrlNameNews(string urlName);
        Task<bool> DeleteNews(int newsId);
        #endregion

        #region news-gallery
        Task AddNewsGalleries(List<NewsGallery>  newsGalleries);
        Task<IList<NewsGallery>> GetAllNewsGalleries(int newsId);

        Task<NewsGallery> GetNewsGalleryImageById(int galleryId);
        Task DeleteNewsGalleryImageById(int id);
        Task<bool> CheckNews(int newsId);

        #endregion
    }
}
