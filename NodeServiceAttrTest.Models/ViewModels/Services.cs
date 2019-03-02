
using System.Collections.Generic;

namespace NodeServiceAttrTest.Models.ViewModels
{
    public class Services
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Attribute> Attributes { get; set; }
        public Node Node { get; set; }
        public Attribute Attribute { get; set; }
    }
}