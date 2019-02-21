using System.Linq;
using NodeServiceAttrTest.Models;

namespace NodeServiceAttrTest.Contracts.Repositories
{
    public interface ICustomDataSetRepository : IRepository<CustomDataSet>
    {
        IDataContext CustomContext { get; }
        IQueryable<CustomDataSet> FromSql(string sql);
    }
}