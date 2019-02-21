using System.Collections.Generic;
using NodeServiceAttrTest.Models;

namespace NodeServiceAttrTest.Contracts.Services
{
    public interface IAttributeService
    {
        IEnumerable<Attribute> GetAll();
        Attribute GetById(int id);
        Attribute Create(Attribute item);
        List<Attribute> CreateRange(List<Attribute> model);
        Attribute Update(Attribute item);
        void Delete(int id);
    }
}
