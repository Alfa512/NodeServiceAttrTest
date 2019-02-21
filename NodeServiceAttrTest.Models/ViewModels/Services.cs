
using System.Collections.Generic;

namespace NodeServiceAttrTest.Models.ViewModels
{
    public class Services
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Node> Nodes { get; set; }
        public IEnumerable<Attribute> Attributes { get; set; }
    }
}