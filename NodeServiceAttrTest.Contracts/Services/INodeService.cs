using System.Collections.Generic;
using NodeServiceAttrTest.Models;

namespace NodeServiceAttrTest.Contracts.Services
{
    public interface INodeService
    {
        IEnumerable<Node> GetAll();
        Node GetById(int id);
        Node Create(Node item);
        List<Node> CreateRange(List<Node> model);
        Node Update(Node item);
        void Delete(int id);
        int Count();
    }
}
