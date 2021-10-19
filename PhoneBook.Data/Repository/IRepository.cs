using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.Repository
{
    public interface IRepository<T> where T : class, new()
    {
        T Get(Expression<Func<T, bool>> match);
        Task<T> GetAsync(Expression<Func<T, bool>> match);

        IList<T> GetAll(Expression<Func<T, bool>> match);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> match);

        Task<IList<T>> GetAllWithPaging(int pageNumber, int pageSize, Expression<Func<T, bool>> match = null);

        T Add(T entity);
        Task<T> AddAsync(T entity);

        List<T> AddRange(List<T> entities);
        Task<List<T>> AddRangeAsync(List<T> entities);

        void Delete(T entity);
        Task DeleteAsync(T entity);

        T Update(T entity, Expression<Func<T, bool>> match);
        Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> match);
        Task UpdateAsync(T entity);


        int Count();
        Task<int> CountAsync();
        Task<int> FilteredCountAsync(Expression<Func<T, bool>> match);

        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        void Dispose();

    }
}
