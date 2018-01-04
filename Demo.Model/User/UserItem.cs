using System.Runtime.Serialization;
using AutoMapper;

namespace Demo.Model.User
{
    [DataContract]
    public class UserItem:Profile
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string Sex { get; set; }

        protected  void Configure()
        {
            CreateMap<UserItem,MyEFDemo.Domain.Entity.User>();
        }
    }
}
