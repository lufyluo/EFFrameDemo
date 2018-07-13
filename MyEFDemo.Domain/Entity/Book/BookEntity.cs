using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFRepository;

namespace MyEFDemo.Domain.Entity.Book
{
    public class BookEntity:BaseEntity
    {
        public override DateTime CreateTime { get; set; }
        public override DateTime UpdateTime { get; set; }
    }
}
