using NodeServiceAttrTest.Contracts;
using NodeServiceAttrTest.Contracts.Repositories;
using NodeServiceAttrTest.Models;

namespace NodeServiceAttrTest.Data.Repositories
{
    public class ServiceNodesRepository : GenericRepository<ServiceNodes>, IServiceNodesRepository
    {
        private IDataContext _context;
        public ServiceNodesRepository(IDataContext context) : base(context)
        {
            _context = context;
        }
    }
}
