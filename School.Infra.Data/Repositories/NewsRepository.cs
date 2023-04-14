using Microsoft.EntityFrameworkCore;
using School.Domain.DTOs.Paging;
using School.Domain.Interaces;
using School.Domain.Models.News;
using Shop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace School.Infra.Data.Repositories
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        #region constructor
        private readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        #endregion

        #region properties

        #region news
        public async Task<IPagedList<News>> FilterNews(RequestParams request)
        {
            var query = _context.News.AsQueryable();
            return await query.AsNoTracking().ToPagedListAsync(request.PageNumber, request.PageSize);


        }
        public async Task<bool> CheckUrlNameNews(string urlName)
        {
            return await _context.News.AsQueryable()
                .AnyAsync(x => x.UrlName == urlName);
        }

        public async Task<bool> DeleteNews(int newsId)
        {
            var news = await _context.News.AsQueryable()
                 .Where(x => x.Id == newsId).SingleOrDefaultAsync();
            if (news == null) return false;
            news.IsDelete = true;
            return true;
        }
        #endregion


        #region news-gallery
        public async Task AddNewsGalleries(List<NewsGallery> newsGalleries)
        {
            await _context.NewsGalleries.AddRangeAsync(newsGalleries);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<NewsGallery>> GetAllNewsGalleries(int newsId)
        {
            var newsgalleries = await _context.NewsGalleries
                .AsQueryable()
                .Where(p => p.NewsId == newsId && !p.IsDelete).ToListAsync();
            return newsgalleries;
        }

        public async Task<NewsGallery> GetNewsGalleryImageById(int galleryId)
        {
            var newsGallery = await _context.NewsGalleries.SingleOrDefaultAsync(i => i.Id == galleryId && !i.IsDelete);
            if (newsGallery != null) return newsGallery;
            return null;
        }

        public async Task DeleteNewsGalleryImageById(int id)
        {
            var cuurentNewsgallery = await _context.NewsGalleries.SingleOrDefaultAsync(i => i.Id == id);
            if (cuurentNewsgallery != null)
            {
                cuurentNewsgallery.IsDelete = true;
                _context.NewsGalleries.Update(cuurentNewsgallery);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<bool> CheckNews(int newsId)
        {
            return await _context.News.AnyAsync(x => x.Id == newsId && !x.IsDelete);
        }
        #endregion


        #endregion
    }
}
