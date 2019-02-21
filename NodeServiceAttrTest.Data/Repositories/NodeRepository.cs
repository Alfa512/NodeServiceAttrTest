using NodeServiceAttrTest.Contracts;
using NodeServiceAttrTest.Contracts.Repositories;
using NodeServiceAttrTest.Models;

namespace NodeServiceAttrTest.Data.Repositories
{
    public class NodeRepository : GenericRepository<Node>, INodeRepository
    {
        private IDataContext _context;
        public NodeRepository(IDataContext context) : base(context)
        {
            _context = context;
        }
    }
}