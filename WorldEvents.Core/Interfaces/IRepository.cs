using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WorldEvents.Core
{
    public interface IRepository<T, TPrimaryKey> where T : Abp.Domain.Entities.Entity<TPrimaryKey>
    {
        T Get(TPrimaryKey id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);

        Task<T> Insert(T entity);
        bool Delete(T entity);
        bool Update(T entity);
    }

    public interface IRepository<T> : IRepository<T, int> where T : Abp.Domain.Entities.Entity<int>
    {
    }
}
