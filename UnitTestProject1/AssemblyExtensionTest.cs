using System;
using System.IO;
using System.Linq;
using System.Reflection;
using EFRepository;
using LufyAssembly.Extension;
using Xunit;

namespace UnitTestProject1
{
    public class AssemblyExtensionTest
    {
        [Fact]
        public void GetAllAssembliesTest()
        {
            var ass = Assembly.GetExecutingAssembly();
           
            var assArr1  = Directory.GetFiles(AssemblyExtension.FindPath(ass), "*.dll").Select(Assembly.LoadFrom).ToArray();

            var typesToRegisterN = assArr1.SelectMany(n => n.GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type =>
                    type.BaseType== typeof(BaseEntity)));
            var assArr = AppDomain.CurrentDomain.GetAssemblies();
            var testArr = ass.GetAllAssemblies();
            Assert.Equal(assArr.Length<= testArr.Length,true);
        }
    }
}
