using IAGenerativa.Data.EF;
using IAGenerativa.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAGenerativa.Data.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
    }
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<Type, object> _repositoriesAsync;
        private Dictionary<Type, object> _repositories;
        public IagenerativaDbContext Context { get; }
        private bool _disposed;

        public UnitOfWork(IagenerativaDbContext context)
        {
            Context = context;
            _disposed = false;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(this);
            }
            return (IRepository<TEntity>)_repositories[type];
        }
        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> SaveAsync()
        {
            return await Context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!_disposed && isDisposing)
            {
                Context.Dispose();
            }
            _disposed = true;
        }
    }
}
