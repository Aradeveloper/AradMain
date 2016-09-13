using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.IService;
using AradCms.Core.Plugin;
using AradCms.Core.Service;
using AutoMapper;
using Microsoft.Owin.Security;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Web;
using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace AradCms.Core.IOC
{
    public class ProjectObjectFactory
    {
        #region Fields

        private static readonly Lazy<Container> _containerBuilder =
            new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        #endregion Fields

        #region Container

        public static IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        #endregion Container

        #region DefaultContainer

        private static Container DefaultContainer()
        {
            var container = new Container(ioc =>
             {
                 ioc.For<IUnitOfWork>()
                     .HybridHttpOrThreadLocalScoped()
                     .Use<ApplicationDbContext>();

                 //ioc.For<IIdentity>().Use(() => HttpContext.Current.User.Identity);
                 ioc.For<IIdentity>().Use(() => getIdentity());
                 ioc.For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));
                 ioc.For<HttpServerUtilityBase>().Use(() => new HttpServerUtilityWrapper(HttpContext.Current.Server));
                 ioc.For<HttpRequestBase>().Use(ctx => ctx.GetInstance<HttpContextBase>().Request);
                 ioc.For<ISessionProvider>().Use<SessionProvider>();
                 ioc.For<IRemotingFormatter>().Use(a => new BinaryFormatter());
                 ioc.For<ITempDataProvider>().Use<CookieTempDataProvider>();

                 ioc.AddRegistry<AspNetIdentityRegistery>();
                 ioc.AddRegistry<AutoMapperRegistery>();
                 ioc.AddRegistry<ServiceLayerRegistery>();

                 ioc.Scan(scanner => scanner.WithDefaultConventions());
                 ioc.Policies.SetAllProperties(y =>
                 {
                     y.OfType<IApplicationUserManager>();
                     y.OfType<IPermissionService>();
                     y.OfType<IAuthenticationManager>();
                 });
                 ioc.Scan(scanner =>
                 {
                     scanner.AssembliesFromPath(
                         path: Path.Combine(HttpRuntime.AppDomainAppPath, "bin"),
                         // یک اسمبلی نباید دوبار بارگذاری شود
                         assemblyFilter: assembly =>
                         {
                             return !assembly.FullName.Equals(typeof(IPlugin).Assembly.FullName);
                         });

                     scanner.WithDefaultConventions(); //Connects 'IName' interface to 'Name' class automatically.
                     scanner.AddAllTypesOf<IPlugin>().NameBy(item => item.FullName);
                 });
             });
            ConfigureAutoMapper(container);
            return container;
        }

        #endregion DefaultContainer

        private static IIdentity getIdentity()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                return HttpContext.Current.User.Identity;
            }

            return ClaimsPrincipal.Current != null ? ClaimsPrincipal.Current.Identity : null;
        }

        private static void ConfigureAutoMapper(IContainer container)
        {
         
            foreach (var profile in container.GetAllInstances<Profile>())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(profile);
                });
            }
            container.GetInstance<IMapper>();
        }

       
    }
}