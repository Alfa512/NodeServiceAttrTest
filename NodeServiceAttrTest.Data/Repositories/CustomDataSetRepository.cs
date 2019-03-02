using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NodeServiceAttrTest.Contracts;
using NodeServiceAttrTest.Contracts.Repositories;
using NodeServiceAttrTest.Models;

namespace NodeServiceAttrTest.Data.Repositories
{
    public class CustomDataSetRepository : GenericRepository<CustomDataSet>, ICustomDataSetRepository
    {
        public CustomDataSetRepository(IDataContext context) : base(context)
        {
        }

        public IQueryable<CustomDataSet> FromSql(string sql)
        {
            return Context.Set<CustomDataSet>().FromSql(sql);
        }
    }
}