using AradCms.Core.Controllers;
using AradCms.Core.Extentions;
using AradCms.Core.Filters;
using AradCms.Core.Model;
using AradCms.Core.Plugin;
using AradCms.Core.WebToolkit;
using StructureMap;
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

namespace CommonInfoPlugin
{
    public class SiteInfoPlugin : IPlugin
    {
        public EfBootstrapper GetEfBootstrapper()
        {
            return new EfBootstrapper
            {
                DomainEntities = new[] { typeof(SiteInfo) },

                DatabaseSeeder = uow =>
                {
                    var model = uow.Set<SiteInfo>();
                    model.Add(new SiteInfo { SiteName = "آراد", SiteAboute = "درباره ما", SiteAddress = "آدرس ما", SiteContact = "تماس با ما", SitePhone = "+989111704858", AboutImage = "", EmailPassword = "", EmailUsername = "", GooglemapCode = "", Smtpserver = "", smtpport = 25 });
                    uow.SaveAllChanges();
                }
            };
        }

        public IList<MenuItem> GetMenuItem(RequestContext requestContext)
        {
            List<MenuItem> model = new List<MenuItem>();
            model.Add(new MenuItem { Name = "اطلاعات وبسایت", Controller = "AdminSiteInfo", Action = "SiteInfo", Area = "SiteInfo", IsAuthorize = true, Slag = "اطلاعات وبسایت", IsWidget = false, Parent = Guid.Empty });
            model.Add(new MenuItem { Name = "اطلاعات وبسایت", Controller = "Widget", Action = "GetHeader", Area = "SiteInfo", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "HeaderZone" });
            model.Add(new MenuItem { Name = "اطلاعات وبسایت", Controller = "Widget", Action = "GetSiteName", Area = "SiteInfo", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "SiteNameZone" });
            model.Add(new MenuItem { Name = "تماس با ما", Controller = "Widget", Action = "Contactus", Area = "SiteInfo", IsAuthorize = false, Slag = "تماس با ما", IsWidget = false, Parent = Guid.Empty, item = "3", WidgetZoneName = "ZoneMainMenu" });
            model.Add(new MenuItem { Name = "اطلاعات وبسایت", Controller = "Widget", Action = "FooterAboute", Area = "SiteInfo", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "FooterAboute" });
            model.Add(new MenuItem { Name = "اطلاعات وبسایت", Controller = "Widget", Action = "ContactFooter", Area = "SiteInfo", IsAuthorize = false, Slag = "", IsWidget = true, Parent = Guid.Empty, WidgetZoneName = "ContactFooter" });

            return model;
        }

        public void RegisterBundles(BundleCollection bundles)
        {
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