﻿namespace NodeServiceAttrTest.Models
{
    public class Attribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}