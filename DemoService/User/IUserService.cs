using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Model.User;
using EFRepository.DependencyManagement;

namespace DemoService.User
{
    public interface IUserService:IDependency
    {
        UserInfo Get(int id);
        int Add(UserItem userItem);
    }
}
