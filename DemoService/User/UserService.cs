using AutoMapper;
using Demo.Model.User;
using EFRepository;
using EFRepository.AutoMapper;

namespace DemoService.User
{
    public class UserService : IUserService
    {
        private IRepository<MyEFDemo.Domain.Entity.User> _repository;

        public UserService(IRepository<MyEFDemo.Domain.Entity.User> repository)
        {
            _repository = repository;
        }
        public UserInfo Get(int id)
        {
            return Mapper.Map<MyEFDemo.Domain.Entity.User, UserInfo>(_repository.Get(id));
        }

        public int Add(UserItem userItem)
        {
            using (var scope = _repository.WorkContext)
            {
                var entity = new MyEFDemo.Domain.Entity.User()
                {
                    Age = userItem.Age,
                    Name = userItem.Name,
                    Sex = userItem.Sex
                };
                _repository.Add(entity);
                scope.Commit();
            }
           
            return 1;
        }
    }
}
