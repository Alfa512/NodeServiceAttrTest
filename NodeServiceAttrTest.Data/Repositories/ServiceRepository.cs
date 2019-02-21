using NodeServiceAttrTest.Contracts;
using NodeServiceAttrTest.Contracts.Repositories;
using NodeServiceAttrTest.Models;

namespace NodeServiceAttrTest.Data.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private IDataContext _context;
        public ServiceRepository(IDataContext context) : base(context)
        {
            _context = context;
        }
    }
}