using IAGenerativa.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IAGenerativa.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {        
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        public async Task<IEnumerable<T>> GetAllAsync() 
        {
            return await _unitOfWork.Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetOne(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {            
            return await _unitOfWork.Context.Set<T>().Where(predicate).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task AddAsync(T entity) 
        {
            if (entity != null)
            {
                await _unitOfWork.Context.Set<T>().AddAsync(entity);
            }
        }

        public async Task Update(object id, T entity) 
        {
            if (entity != null)
            {
                T entitytoUpdate = await _unitOfWork.Context.Set<T>().FindAsync(id);
                if (entitytoUpdate != null)
                {
                    _unitOfWork.Context.Set<T>().Update(entity);
                }
            }
        }

        public async Task Delete(object id) 
        {
            T entity = await _unitOfWork.Context.Set<T>().FindAsync(id);
            if (entity != null) 
            {
                _unitOfWork.Context.Set<T>().Remove(entity);
            }            
        }
        public async Task<IEnumerable<T>> GetAllAsync(string includeProperties)
        {
            IQueryable<T> query = _unitOfWork.Context.Set<T>();
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }
            return await query.ToListAsync();
        }

    }
}
