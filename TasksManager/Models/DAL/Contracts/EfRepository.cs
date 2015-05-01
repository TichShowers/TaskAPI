using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TasksManager.Models.DAL.Contracts
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public EfRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        public IList<T> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(int id)
        {
            T t = GetById(id);
            if (t != null)
            {
                Delete(t);
            }
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public virtual T GetLastInserted()
        {
            return DbSet.LastOrDefault();
        }
    }
}