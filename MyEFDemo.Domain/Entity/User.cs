using EFRepository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using EFRepository.Attributes;

namespace MyEFDemo.Domain.Entity
{
    public class User:BaseEntity
    {
        public Guid Guid { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        //[DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed)]  code first 无效
        public override DateTime CreateTime { get; set; }

        public override DateTime UpdateTime { get; set; }
    }
}
