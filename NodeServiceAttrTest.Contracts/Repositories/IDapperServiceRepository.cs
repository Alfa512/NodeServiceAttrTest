using System.Linq;
using NodeServiceAttrTest.Models;

namespace NodeServiceAttrTest.Contracts.Repositories
{
    public interface IDapperServiceRepository : IRepository<Service>
    {
        IQueryable<Models.ViewModels.Services> GetServicesTop(int top);
        IQueryable<Models.ViewModels.Services> GetServices();
    }
}