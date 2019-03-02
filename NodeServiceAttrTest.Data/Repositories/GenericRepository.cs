using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NodeServiceAttrTest.Contracts;
using NodeServiceAttrTest.Contracts.Repositories;

namespace NodeServiceAttrTest.Data.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly IDataContext Context;

        protected GenericRepository(IDataContext context)
        {
            Context = context;
        }

        public IQueryable<T> GetAll()
        {
            
            return Context.Set<T>();
        }

        public T Create(T entity)
        {
            return Context.Set<T>().Add(entity).Entity;
        }

        public void CreateRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public T Update(T entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Context.Set<T>().Attach(entity);
                entry = Context.Entry(entity);
            }

            entry.State = EntityState.Modified;
            return entity;
        }

        public T Delete(T entity)
        {
            return Context.Set<T>().Remove(entity).Entity;
        }

        public void SaveCganges()
        {
            Context.SaveChanges();
        }

        public void SaveChangesAsync()
        {
            Context.SaveChangesAsync();
        }
    }
}