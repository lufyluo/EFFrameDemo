using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Demo.Model.User;
using EFRepository;
using EFRepository.AutoMapper;
using MyEFDemo.Domain.Entity.Repo;

namespace DemoService.User
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public UserInfo Get(int id)
        {
            return Mapper.Map<MyEFDemo.Domain.Entity.User, UserInfo>(_repository.Get(id));
        }

        public int Add(UserItem userItem)
        {
            throw new NotImplementedException();
        }
        public int Add(IList<UserItem> userItem)
        {
            using (var scope = _repository.WorkContext)
            {
                var entities = userItem.Select(n => new MyEFDemo.Domain.Entity.User()
                {
                    Guid = Guid.NewGuid(),
                    Age = n.Age,
                    Name = n.Name,
                    Sex = n.Sex,
                    CreateTime = DateTime.Now
                }).ToList();
                _repository.Add(entities);
                scope.Commit();
            }
           
            return 1;
        }

        public void Update(IList<UserItem> userItem)
        {

            using (var scope = _repository.WorkContext)
            {
                _repository.Update(n => userItem.Select(u=>u.Name).Contains(n.Name), m => new MyEFDemo.Domain.Entity.User()
                {
                    CreateTime = DateTime.Now
                });
                scope.Commit();
            }
        }
    }
}
