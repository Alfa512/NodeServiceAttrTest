using System.Collections.Generic;

namespace NodeServiceAttrTest.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ServiceNodes> ServiceNodes { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
    }
}