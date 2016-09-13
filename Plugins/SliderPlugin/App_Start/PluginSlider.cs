using AradCms.Core.Controllers;
using AradCms.Core.Extentions;
using AradCms.Core.Filters;
using AradCms.Core.Model;
using AradCms.Core.Plugin;
using AradCms.Core.WebToolkit;
using SliderPlugin.Configuration;
using SliderPlugin.IService;
using SliderPlugin.Models;
using SliderPlugin.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SliderPlugin
{
    public class PluginSlider : IPlugin
    {
        public EfBootstrapper GetEfBootstrapper()
        {
            return new EfBootstrapper
            {
                DomainEntities = new[] { typeof(SliderModel) },
                ConfigurationsAssemblies = new[]
                {
                    typeof(SliderModelConfig).Assembly
                },
                DatabaseSeeder = uow =>
                {
                }
            };
        }

        public IList<MenuItem> GetMenuItem(RequestContext requestContext)
        {
            List<MenuItem> model = new List<MenuItem>();
            model.Add(new MenuItem { Name = "اسلایدر", Controller = "SliderWidget", Action = "SliderShow", Area = "Sliders", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "SliderZone" });
            model.Add(new MenuItem { Name = "اسلایدر", Controller = "AdminSlider", Action = "List", Area = "Sliders", IsAuthorize = true, Slag = "اسلاید", IsWidget = false, Parent = Guid.Empty });

            return model;
        }

        public void RegisterBundles(BundleCollection bundles)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            // Mostly the default namespace and assembly name are the same
            var assemblyNameSpace = executingAssembly.GetName().Name;

            var scriptsBundle = new Bundle("~/SliderPlugin/Scripts",
                new EmbeddedResourceTransform(new List<string>
                {
                    assemblyNameSpace+".Scripts.jquery.sider.js"
                }, "application/javascript", executingAssembly));

            if (!HttpContext.Current.IsDebuggingEnabled)
            {
                scriptsBundle.Transforms.Add(new JsMinify());
            }

            bundles.Add(scriptsBundle);

            BundleTable.EnableOptimizations = true;
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            //todo: add custom routes.

            var assembly = Assembly.GetExecutingAssembly();
            // Mostly the default namespace and assembly name are the same
            var nameSpace = assembly.GetName().Name;
            var resourcePath = string.Format("{0}.Images", nameSpace);

            routes.Insert(0,
                new Route("SliderPlugin/Images/{file}.{extension}",
                    new RouteValueDictionary(new { }),
                    new RouteValueDictionary(new { extension = "png|jpg" }),
                    new EmbeddedResourceRouteHandler(assembly, resourcePath, cacheDuration: TimeSpan.FromDays(30))
                ));
        }

        public void RegisterServices(StructureMap.IContainer container)
        {
            // todo: add custom services.

            container.Configure(cfg =>
            {
                cfg.For<ISliderService>().Use<SliderService>();
            });
        }

        public IEnumerable<ApplicationPermission> ConfigPermissions()
        {
            var controllers =
                   Assembly.GetExecutingAssembly().GetTypes()
                       .Where(
                           t =>
                               t.BaseType == typeof(BaseController))
                       .ToList();

            var permissionsListToAdd = new List<ApplicationPermission>();

            foreach (var controller in controllers)
            {
                var controllerName = controller.Name.Replace("Controller", "").ToLower();

                var actionMethodsList =
                    controller.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        .Where(method => (typeof(ActionResult).IsAssignableFrom(method.ReturnType) ||
                                          typeof(Task<ActionResult>).IsAssignableFrom(method.ReturnType))
                                         &&
                                         method.CustomAttributes.All(
                                             a => a.AttributeType != typeof(ChildActionOnlyAttribute))
                                         &&
                                         method.CustomAttributes.All(
                                             a => a.AttributeType != typeof(AllowAnonymousAttribute))
                                         &&
                                         method.CustomAttributes.Any(
                                             a => a.AttributeType == typeof(DisplayNameAttribute))
                                         &&
                                         method.CustomAttributes.Any(
                                             a => a.AttributeType == typeof(AradAuthorizeAttribute)))
                        .ToList();

                permissionsListToAdd.AddRange(from methodInfo in actionMethodsList
                                              let actionName = methodInfo.Name.ToLower()
                                              let displayName = methodInfo.GetCustomAttribute<DisplayNameAttribute>().DisplayName
                                              let authorizeAttribute = methodInfo.GetCustomAttribute<AradAuthorizeAttribute>()
                                              let areaName = authorizeAttribute.AreaName.IsNotEmpty() ? authorizeAttribute.AreaName.ToLower() : ""
                                              select new ApplicationPermission
                                              {
                                                  Description = displayName,
                                                  AreaName = areaName,
                                                  ControllerName = controllerName,
                                                  ActionName = actionName,
                                                  IsMenu = authorizeAttribute.IsMenu,
                                                  PName = authorizeAttribute.Roles
                                              });
            }

            return permissionsListToAdd;
        }
    }
}