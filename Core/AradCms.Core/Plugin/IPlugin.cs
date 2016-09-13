using AradCms.Core.Context;
using AradCms.Core.Model;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Optimization;
using System.Web.Routing;

namespace AradCms.Core.Plugin
{
    public interface IPlugin
    {
        EfBootstrapper GetEfBootstrapper();

        IList<MenuItem> GetMenuItem(RequestContext requestContext);

        void RegisterBundles(BundleCollection bundles);

        void RegisterRoutes(RouteCollection routes);

        void RegisterServices(IContainer container);

        IEnumerable<ApplicationPermission> ConfigPermissions();
    }

    public class EfBootstrapper
    {
        /// <summary>
        /// Assemblies containing EntityTypeConfiguration classes.
        /// </summary>
        public Assembly[] ConfigurationsAssemblies { get; set; }

        /// <summary>
        /// Domain classes.
        /// </summary>
        public Type[] DomainEntities { get; set; }

        /// <summary>
        /// Custom Seed method.
        /// </summary>
        public Action<IUnitOfWork> DatabaseSeeder { get; set; }
    }
}