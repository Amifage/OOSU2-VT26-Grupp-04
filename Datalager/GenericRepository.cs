using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entitetslager;
using Microsoft.EntityFrameworkCore;


namespace Datalager
{
    public class GenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        #region Generella metoder

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }


        public T? HämtaId(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public void Add(T entity) 
        {
           _dbSet.Add(entity);
        }

        public void Remove(T entity) 
        {
         _dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll() //Ändrad
        {
            return _dbSet.ToList();
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) //NY
        {
            return _dbSet.Where(predicate).ToList();
        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public bool IsEmpty()
        {
            return _dbSet.Count() == 0;
        }
        public int Count()
        {
            return _dbSet.Count();
        }
        #endregion
    }
}
