using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFRepository;

namespace MyEFDemo.Domain.Entity.Repo
{
    interface IUserRepository : IRepository<MyEFDemo.Domain.Entity.User>
    {
    }
}
