using AradCms.Core.IOC;
using AradCms.Core.Plugin;
using AradMain;
using System.Linq;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(PluginsStart), "Start")]

namespace AradMain
{
    public static class PluginsStart
    {
        public static void Start()
        {
            var plugins = ProjectObjectFactory.Container.GetAllInstances<IPlugin>().ToList();
            foreach (var plugin in plugins)
            {
                plugin.RegisterServices(ProjectObjectFactory.Container);
                plugin.RegisterRoutes(RouteTable.Routes);
                plugin.RegisterBundles(BundleTable.Bundles);
            }
        }
    }
}