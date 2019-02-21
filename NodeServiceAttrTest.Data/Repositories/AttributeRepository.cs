using NodeServiceAttrTest.Contracts;
using NodeServiceAttrTest.Contracts.Repositories;
using NodeServiceAttrTest.Models;

namespace NodeServiceAttrTest.Data.Repositories
{
    public class AttributeRepository : GenericRepository<Attribute>, IAttributeRepository
    {
        private IDataContext _context;
        public AttributeRepository(IDataContext context) : base(context)
        {
            _context = context;
        }
    }
}