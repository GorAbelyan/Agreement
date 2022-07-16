using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProprties = null
            );
        T GetFirstOrDefult(
            Expression<Func<T, bool>> filter = null,
            string includeProprties = null
            );

        T GetLastOrDefult(
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        Expression<Func<T, bool>> filter = null,
        string includeProprties = null
        );

        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

    }
}
