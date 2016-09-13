using AradCms.Core.Model;
using AradCms.Core.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace CommonInfoPlugin.App_Start
{
    public class CommonPlugin : IPlugin
    {
        public IEnumerable<ApplicationPermission> ConfigPermissions()
        {
            throw new NotImplementedException();
        }

        public EfBootstrapper GetEfBootstrapper()
        {
            throw new NotImplementedException();
        }

        public IList<MenuItem> GetMenuItem(RequestContext requestContext)
        {
            throw new NotImplementedException();
        }

        public void RegisterBundles(BundleCollection bundles)
        {
            throw new NotImplementedException();
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            throw new NotImplementedException();
        }

        public void RegisterServices(global::StructureMap.IContainer container)
        {
            throw new NotImplementedException();
        }
    }
}