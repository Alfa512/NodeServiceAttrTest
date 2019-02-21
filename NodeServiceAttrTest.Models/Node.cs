using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NodeServiceAttrTest.Models
{
    [Table("UINode")]
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public ICollection<ServiceNodes> ServiceNodes { get; set; }
    }
}