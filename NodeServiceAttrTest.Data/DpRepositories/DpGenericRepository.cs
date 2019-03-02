using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using NodeServiceAttrTest.Contracts.Repositories;

namespace NodeServiceAttrTest.Data.DpRepositories
{
    public abstract class DpGenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly string ConnectionString;

        protected DpGenericRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> entries;
            // ToDo Table names to ENUM or parse name correctly
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                entries = db.Query<T>("SELECT * FROM " + typeof(T)).AsQueryable();
            }
            return entries;
        }

        public T Create(T entry)
        {
            // ToDo Not implemented

            return entry;
        }

        public void CreateRange(IEnumerable<T> entities)
        {
            // ToDo Not implemented
        }

        public T Update(T entry)
        {
            // ToDo Not implemented

            return entry;
        }

        public T Delete(T entity)
        {
            // ToDo Not implemented

            return entity;
        }

        public void SaveCganges()
        {
        }

        public void SaveChangesAsync()
        {
        }
    }
}