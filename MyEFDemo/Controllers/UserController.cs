using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Demo.Model.User;
using DemoService.User;

namespace MyEFDemo.Controllers
{
    public class UserController : ApiController
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public UserInfo Get(int id)
        {
            return _userService.Get(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IList<UserItem> values)
        {
            return _userService.Add(values);
        }

        // PUT api/<controller>/5
        public void Put([FromBody]IList<UserItem> values)
        {
             _userService.Update(values);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}