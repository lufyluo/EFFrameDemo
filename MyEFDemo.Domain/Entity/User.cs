using EFRepository;
using System;

namespace MyEFDemo.Domain.Entity
{
    public class User:BaseEntity
    {
        public Guid Guid { get; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
    }
}
