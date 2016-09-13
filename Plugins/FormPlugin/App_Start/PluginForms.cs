using AradCms.Core.Controllers;
using AradCms.Core.Extentions;
using AradCms.Core.Filters;
using AradCms.Core.Model;
using AradCms.Core.Plugin;
using AradCms.Core.WebToolkit;
using FormPlugin.Configoration;
using FormPlugin.IService;
using FormPlugin.Models;
using FormPlugin.Service;
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

namespace FormPlugin
{
    public class PluginForms : IPlugin
    {
        public EfBootstrapper GetEfBootstrapper()
        {
            return new EfBootstrapper
            {
                DomainEntities = new[] { typeof(OrderForm), typeof(Receipt), typeof(SendingFile) },
                ConfigurationsAssemblies = new[]
                {
                    typeof(OrderFormConfig).Assembly,
                    typeof(ReceiptConfig).Assembly,
                    typeof(SendingFileConfig).Assembly
                },
                //DatabaseSeeder = uow =>
                //{
                //    var blogtype = uow.Set<BlogType>();

                //    var modelblogtype = blogtype.Add(new BlogType { Name = "بلاگ" });
                //    var modelblogtype2 = blogtype.Add(new BlogType { Name = "خبرها" });
                //    var modelblogtype3 = blogtype.Add(new BlogType { Name = "بیشتر" });
                //    var blog = uow.Set<Blog>();
                //    blog.Add(new Blog { Name = "بلاگ", BlogType = modelblogtype, Slug = "بلاگ", IsActive = true });
                //    blog.Add(new Blog { Name = "خبرها", BlogType = modelblogtype2, Slug = "خبرها", IsActive = true });
                //    blog.Add(new Blog { Name = "بیشتر", BlogType = modelblogtype3, Slug = "بیشتر", IsActive = true });
                //}
            };
        }

        public IList<MenuItem> GetMenuItem(RequestContext requestContext)
        {
            List<MenuItem> model = new List<MenuItem>();
            //model.Add(new MenuItem { Name = "بلاگ", Controller = "AdminBlog", Action = "List", Area = "Blog", IsAuthorize = true, Slag = "بلاگ", IsWidget = false, Parent = Guid.Empty });
            //model.Add(new MenuItem { Name = "نمایش محتوای بلاگ", Controller = "Default", Action = "GetBlogTop", Area = "Blog", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "Slider" });
            //model.Add(new MenuItem { Name = "نمایش محتوای بلاگ", Controller = "Default", Action = "GetBlogCenter", Area = "Blog", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "Center" });
            //model.Add(new MenuItem { Name = "نمایش محتوای بلاگ", Controller = "Default", Action = "GetBlogFooter", Area = "Blog", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "BlogFooter" });
            return model;
        }

        public void RegisterBundles(BundleCollection bundles)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            // Mostly the default namespace and assembly name are the same
            var assemblyNameSpace = executingAssembly.GetName().Name;

            var scriptsBundle = new Bundle("~/FormPlugin/Scripts",
                new EmbeddedResourceTransform(new List<string>
                {
                    assemblyNameSpace+".Scripts.jquery-2.2.4.js"
                }, "application/javascript", executingAssembly));

            if (!HttpContext.Current.IsDebuggingEnabled)
            {
                scriptsBundle.Transforms.Add(new JsMinify());
            }

            bundles.Add(scriptsBundle);

            var cssBundle = new Bundle("~/FormPlugin/Content",
                new EmbeddedResourceTransform(new List<string>
                {
                    assemblyNameSpace + ".Content.Site.css"
                }, "text/css", executingAssembly));

            if (!HttpContext.Current.IsDebuggingEnabled)
            {
                cssBundle.Transforms.Add(new CssMinify());
            }

            bundles.Add(cssBundle);

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
                new Route("BlogPlugin/Images/{file}.{extension}",
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
                cfg.For<IOrderService>().Use<OrderService>();
                cfg.For<IReceiptService>().Use<RecieptService>();
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