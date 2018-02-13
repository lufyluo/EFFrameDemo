using EFRepository;
using System;
using EFRepository.Attributes;

namespace MyEFDemo.Domain.Entity
{
    public class User:BaseEntity
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        [DbTime]
        public override DateTime CreateTime { get; set; }
    }
}
