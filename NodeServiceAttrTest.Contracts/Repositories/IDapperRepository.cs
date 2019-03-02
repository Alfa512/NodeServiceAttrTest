using System.Collections.Generic;
using System.Linq;

namespace NodeServiceAttrTest.Contracts.Repositories
{
    public interface IDapperRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Create(T entity);
        void CreateRange(IEnumerable<T> entities);
        T Update(T entity);
        T Delete(T entity);
        void SaveCganges();
        void SaveChangesAsync();
    }
}