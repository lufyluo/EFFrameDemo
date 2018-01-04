using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DemoService.Value;

namespace MyEFDemo.App_Start
{
    public class AutofacWebApiConfig
    {
        public  void Run()
        {
            new AutofacWebApiConfig().SetAutofacWebApi();
        }

        protected  void SetAutofacWebApi()
        {
            ContainerBuilder builder = new ContainerBuilder();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            // Register API controllers using assembly scanning.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ValueService>().As<IValueService>()
                .InstancePerApiRequest();
            var container = builder.Build();
            // Set the WebApi dependency resolver.
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}