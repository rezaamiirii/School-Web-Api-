using Microsoft.EntityFrameworkCore;
using School.Domain.Interaces;
using Shop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infra.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region constructor

        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        #endregion



        public async Task Add(T entity)
        {
           await _context.AddAsync(entity);

        }

        public async Task Delete(T entity)
        {
             _context.Set<T>().Remove(entity);
        }

        public async Task<bool> Exist(int id)
        {
           var entity= await Get(id);
            return entity!=null;
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
