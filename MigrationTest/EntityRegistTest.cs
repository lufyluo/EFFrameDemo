using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using EFRepository;
using Xunit;

namespace MigrationTest
{
    public class EntityRegistTest
    {
        [Fact]
        public void RegistCompleteTest()
        {
            List<BaseEntity> entities = new List<BaseEntity>();
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                entities.Add(configurationInstance);
            }

           
        }
    }
}
