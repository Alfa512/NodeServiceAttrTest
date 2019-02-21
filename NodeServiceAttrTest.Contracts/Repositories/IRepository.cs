using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NodeServiceAttrTest.Contracts.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        EntityEntry<T> Create(T entity);
        void CreateRange(IEnumerable<T> entities);
        T Update(T entity);
        EntityEntry<T> Delete(T entity);
        void SaveCganges();
        void SaveChangesAsync();
    }
}