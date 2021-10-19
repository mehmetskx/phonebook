using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.Repository.EFDerived
{
    public class EFCoreRepository<T, TId> : IRepository<T> where T : class, new()
    {
        #region Fields

        protected readonly DbContext Context;
        protected DbSet<T> Set { get; }

        private DbSet<T> _entities;

        #endregion

        #region Ctor

        public EFCoreRepository(DbContext context)
        {
            this.Context = context;
            Set = Context.Set<T>();
        }

        #endregion

        #region Properties

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = Context.Set<T>();
                return _entities;
            }
        }

        #endregion

        #region Methods

        public T Add(T entity)
        {
            if (entity == null)
                return null;

            Entities.Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
                return null;

            await Entities.AddAsync(entity);
            return entity;
        }

        public List<T> AddRange(List<T> entities)
        {
            if (entities.Count <= 0)
                return null;

            Entities.AddRange(entities);
            return entities;
        }

        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            if (entities.Count <= 0)
                return null;

            await Entities.AddRangeAsync(entities);
            return entities;
        }

        public T Update(T entity, Expression<Func<T, bool>> match)
        {
            var existingEntity = Entities.FirstOrDefault(match);

            if (existingEntity == null)
                return null;

            Entities.Update(entity);

            return existingEntity;
        }

        public async Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> match)
        {
            var existingEntity = await Entities.FirstOrDefaultAsync(match);
            if (existingEntity == null)
                return null;

            Entities.Update(entity);

            return existingEntity;
        }
        public virtual async Task UpdateAsync(T entity)
        {
            Entities.Update(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
                return;

            if (entity is BaseEntity<TId>)
            {
                var _entity = entity as BaseEntity<TId>;
                _entity.IsActive = false;
                Entities.Update(_entity as T);
            }
        }

        public Task DeleteAsync(T entity)
        {
            if (entity == null)
                return Task.CompletedTask;

            if (entity is BaseEntity<TId>)
            {
                var _entity = entity as BaseEntity<TId>;
                _entity.IsActive = false;
                Entities.Update(_entity as T);
            }

            return Task.CompletedTask;
        }

        public T Get(Expression<Func<T, bool>> match)
        {
            return Entities.FirstOrDefault(match);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> match)
        {
            return await Entities.FirstOrDefaultAsync(match);
        }

        public IList<T> GetAll(Expression<Func<T, bool>> match)
        {
            return Table.Where(match).ToList();
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> match)
        {
            return await Entities.Where(match).ToListAsync();
        }

        public async Task<IList<T>> GetAllWithPaging(int pageNumber, int pageSize, Expression<Func<T, bool>> match = null)
        {
            IQueryable<T> queryable = Table;
            int skipCount = pageNumber * pageSize;
            if (match != null)
            {
                queryable = queryable.Where(match);
            }
            var result = queryable.Skip(skipCount).Take(pageSize);
            return await result.ToListAsync();
        }

        public int Count()
        {
            return Entities.Count();
        }

        public async Task<int> CountAsync()
        {
            return await Entities.CountAsync();
        }
        public async Task<int> FilteredCountAsync(Expression<Func<T, bool>> match)
        {
            return await Entities.Where(match).CountAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = Table;
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.AsNoTracking().Include(includeProperty);
            }

            return queryable;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       
        #endregion
    }
}
