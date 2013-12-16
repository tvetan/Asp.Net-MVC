using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCopy.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        bool Any(Expression<Func<T, bool>> predicate);

        int Count { get; }

        void Create(T entity);
      
        T GetById(int id);

  
        void Update(T entity);

        int Delete(Expression<Func<T, bool>> predicate);
        void Delete(T entity);
        void Delete(int id);

        void Detach(T entity);

        T Find(params object[] keys);
        T Find(Expression<Func<T, bool>> predicate);
        
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, int index, int size);
    }
}