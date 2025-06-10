using IAGenerativa.Data.EF;
using IAGenerativa.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAGenerativa.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        //IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;
        IagenerativaDbContext Context { get; }
        Task<bool> SaveAsync();
    }
}
