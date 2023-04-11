using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Interaces
{
    public interface IGenericRepository<T> where T : class
    {

        Task<T> Get(int id);
        Task<IReadOnlyList<T>> GetAll();

        Task Add(T entity);
        void Update(T entity);
        Task Delete(T entity);
        
        Task<bool> Exist(int id);

        Task SaveChanges();
    }
}
