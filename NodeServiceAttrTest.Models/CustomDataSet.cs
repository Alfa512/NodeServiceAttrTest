namespace NodeServiceAttrTest.Models
{
    public class CustomDataSet
    {
        public int Id { get; set; } // Service.Id
        public string Name { get; set; } // Service.Name
        public int NodeId { get; set; }
        public string NodeName { get; set; }
        public int ParentId { get; set; } // Node.ParentId
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        //public int ServiceId { get; set; } // Attribute.ServiceId

    }
}