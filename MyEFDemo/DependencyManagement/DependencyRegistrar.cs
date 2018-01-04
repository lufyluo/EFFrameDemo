using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DemoService.User;
using EFRepository;
using EFRepository.DependencyManagement;
using MyEFDemo.Domain.Entity;

namespace MyEFDemo.DependencyManagement
{
    public class DependencyRegistrar: IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, DemoConfig config)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll").Select(Assembly.LoadFrom).ToArray();
            Type baseType = typeof(IDependency);
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                .AsSelf().AsImplementedInterfaces()
                .PropertiesAutowired().InstancePerLifetimeScope();
        }
        public  static AutofacWebApiDependencyResolver Register()
        {
            var builder = new ContainerBuilder();
            new DependencyRegistrar().Register(builder,null,null);
            return new AutofacWebApiDependencyResolver(builder.Build());
        }

        public int Order { get; }
    }
}