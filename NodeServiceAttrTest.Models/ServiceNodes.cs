namespace NodeServiceAttrTest.Models
{
    public class ServiceNodes
    {
        public int NodeId { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public Node Node { get; set; }
    }
}