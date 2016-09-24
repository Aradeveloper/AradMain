using AradCms.Core.Controllers;
using AradCms.Core.Extentions;
using AradCms.Core.Filters;
using AradCms.Core.Model;
using AradCms.Core.Plugin;
using AradCms.Core.WebToolkit;
using BlogPlugin.Configuration;
using BlogPlugin.IService;
using BlogPlugin.Models;
using BlogPlugin.Service;
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

namespace BlogPlugin
{
    public class PluginBlog : IPlugin
    {
        public EfBootstrapper GetEfBootstrapper()
        {
            return new EfBootstrapper
            {
                DomainEntities = new[] { typeof(Blog), typeof(BlogPost), typeof(PostComment), typeof(Tag) },
                ConfigurationsAssemblies = new[]
                {
                    typeof(BlogConfig).Assembly,
                    typeof(PostCommentConfig).Assembly,
                    typeof(TagConfig).Assembly,
                    typeof(BlogPostConfig).Assembly
                },
                DatabaseSeeder = uow =>
                {
                    var blog = uow.Set<Blog>();
                    blog.Add(new Blog { Name = "بلاگ", Slug = "بلاگ", IsActive = true });
                    blog.Add(new Blog { Name = "خبرها", Slug = "خبرها", IsActive = true });
                    blog.Add(new Blog { Name = "بیشتر", Slug = "بیشتر", IsActive = true });
                    blog.Add(new Blog { Name = "خدمات", Slug = "خدمات", IsActive = true });
                    blog.Add(new Blog { Name = "آموزش", Slug = "آموزش", IsActive = true });
                    uow.SaveAllChanges();
                }
            };
        }

        public IList<MenuItem> GetMenuItem(RequestContext requestContext)
        {
            List<MenuItem> model = new List<MenuItem>();
            model.Add(new MenuItem { Name = "بلاگ", Controller = "AdminBlog", Action = "List", Area = "Blog", IsAuthorize = true, Slag = "بلاگ", IsWidget = false, Parent = Guid.Empty });
            model.Add(new MenuItem { Name = "نمایش محتوای بلاگ", Controller = "Widget", Action = "GetBlogList", Area = "Blog", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "ZoneBlogList" });
            model.Add(new MenuItem { Name = "نمایش خدمات در صفحه اول", Controller = "Widget", Action = "GetServiceList", Area = "Blog", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "ZoneServiceList" });
            model.Add(new MenuItem { Name = "نمایش محتوای بلاگ", Controller = "Widget", Action = "GetBlogFooter", Area = "Blog", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "ZoneFooter" });
            model.Add(new MenuItem { Name = "بلاگ", Controller = "Widget", Action = "GetList", Area = "Blog", IsAuthorize = false, Slag = "بلاگ", IsWidget = false, Parent = Guid.Empty, item = "1", WidgetZoneName = "ZoneMainMenu" });
            model.Add(new MenuItem { Name = "خبر", Controller = "Widget", Action = "GetList", Area = "Blog", IsAuthorize = false, Slag = "خبر", IsWidget = false, Parent = Guid.Empty, item = "2", WidgetZoneName = "ZoneMainMenu" });
            model.Add(new MenuItem { Name = "خبرهای آراد", Controller = "Widget", Action = "GetList", Area = "Blog", IsAuthorize = false, Slag = "خبرهای آراد", IsWidget = false, Parent = Guid.Empty, item = "3", WidgetZoneName = "ZoneMainMenu" });
            model.Add(new MenuItem { Name = "خدمات", Controller = "Widget", Action = "GetList", Area = "Blog", IsAuthorize = false, Slag = "خدمات", IsWidget = false, Parent = Guid.Empty, item = "4", WidgetZoneName = "ZoneMainMenu" });
            model.Add(new MenuItem { Name = "آموزش", Controller = "Widget", Action = "GetList", Area = "Blog", IsAuthorize = false, Slag = "آموزش", IsWidget = false, Parent = Guid.Empty, item = "5", WidgetZoneName = "ZoneMainMenu" });
            model.Add(new MenuItem { Name = "پربازدید ها", Controller = "Widget", Action = "GetPopullore", Area = "Blog", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "sideboxwidget" });
            model.Add(new MenuItem { Name = "پربازدید ها", Controller = "Widget", Action = "GetBlogSidebar", Area = "Blog", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "sideboxwidget" });
            model.Add(new MenuItem { Name = "پربازدید فوتر", Controller = "Widget", Action = "GetFooterNews", Area = "Blog", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "GetFooterNews" });
            return model;
        }

        public void RegisterBundles(BundleCollection bundles)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            // Mostly the default namespace and assembly name are the same
            var assemblyNameSpace = executingAssembly.GetName().Name;

            var scriptsBundle = new Bundle("~/BlogPlugin/Scripts",
                new EmbeddedResourceTransform(new List<string>
                {
                    assemblyNameSpace+".Scripts.jquery-1.10.2.js",
                    assemblyNameSpace+".Scripts.prettify.js",
                    assemblyNameSpace+".Scripts.jquery.jatt.min.js",
                    assemblyNameSpace+".Scripts.jquery.anythingslider.min.js",
                    assemblyNameSpace+".Scripts.jquery.anythingslider.video.js"
                }, "application/javascript", executingAssembly));

            if (!HttpContext.Current.IsDebuggingEnabled)
            {
                scriptsBundle.Transforms.Add(new JsMinify());
            }

            bundles.Add(scriptsBundle);

            var cssBundle = new Bundle("~/BlogPlugin/Content",
                new EmbeddedResourceTransform(new List<string>
                {
                    assemblyNameSpace + ".Content.prettify.css",
                    assemblyNameSpace + ".Content.page.css",
                    assemblyNameSpace + ".Content.anythingslider.css",

                    assemblyNameSpace + ".Content.theme-minimalist-round.css",

                    assemblyNameSpace + ".Content.animate.css"
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
                cfg.For<IBlogService>().Use<BlogService>();
                cfg.For<IPostService>().Use<PostService>();
                cfg.For<ICommentService>().Use<ComentService>();
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