using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldEvents.DBModel;

namespace WorldEvents.Core
{
    public class Repository<T, TPrimaryKey> : IRepository<T, TPrimaryKey> where T : Abp.Domain.Entities.Entity<TPrimaryKey>
    {
        private readonly SatteliteDbContext _context;
        private readonly DbSet<T> _entities;
        private string errorMessage = string.Empty;

        public Repository(SatteliteDbContext context)
        {
            this._context = context;
            _entities = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _entities;
        }

        public T Get(TPrimaryKey id)
        {
            return _entities.SingleOrDefault(s => s.Id.Equals(id));
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = GetAll();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public async Task<T> Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var newEntity = await _entities.AddAsync(entity);
            int saved = _context.SaveChanges();

            return newEntity.Entity;
        }

        public bool Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var result = _entities.Update(entity);

            return _context.SaveChanges() == 1;
        }

        public bool Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
            _context.SaveChanges();

            return true;
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);            
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

    public class Repository<T> : Repository<T, int>/*, IRepository<T, int>*/ where T : Abp.Domain.Entities.Entity<int>
    {
        public Repository(SatteliteDbContext context) : base(context)
        {
        }
    }
}  

