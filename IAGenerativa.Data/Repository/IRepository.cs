using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IAGenerativa.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(string includeProperties);
        Task<T> GetOne(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task Update(object id, T entity);
        Task Delete(object id);        
    }
}
