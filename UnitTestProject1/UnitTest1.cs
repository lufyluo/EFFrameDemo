using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using EFRepository;
using Xunit;

namespace UnitTestProject1
{
    public class UnitTest1
    {
      [Fact]
        public void TestMethod1()
      {
            var assemblies1 = Assembly.GetExecutingAssembly().GetReferencedAssemblies();

            var path = Assembly.GetExecutingAssembly().CodeBase;

            System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var dllPath = @"E:\GitProject\privateProject\EFFrameDemo\MyEFDemo.Domain\bin\Debug\MyEFDemo.Domain.dll";
            System.Reflection.Assembly assembly = Assembly.LoadFile(dllPath);
            AppDomain.CurrentDomain.Load(assembly.GetName());

            Assembly[] assemblies2 = AppDomain.CurrentDomain.GetAssemblies();


            var typesToRegisterN = assemblies.SelectMany(n => n.GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type =>
                    type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() ==
                    typeof(BaseEntity)));


            var typesToRegister = assemblies.SelectMany(n => n.GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type =>
                    type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() ==
                    typeof(EntityTypeConfiguration<>)));

            List<BaseEntity> entities = new List<BaseEntity>();
            //var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            //    .Where(type => !String.IsNullOrEmpty(type.Namespace))
            //    .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                entities.Add(configurationInstance);
            }
            Assert.Empty(entities);
        }
    }
    public class De
    {
        public DateTime dtTime { get; set; }
       public int age { get; set; }
    }
}
