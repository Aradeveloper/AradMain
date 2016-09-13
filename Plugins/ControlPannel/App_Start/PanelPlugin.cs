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

namespace ControlPannel
{
    public class PanelPlugin : IPlugin
    {
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

        public EfBootstrapper GetEfBootstrapper()
        {
            return null;
        }

        public IList<MenuItem> GetMenuItem(RequestContext requestContext)
        {
            List<MenuItem> model = new List<MenuItem>();
            model.Add(new MenuItem { Name = "پنل", Controller = "Default", Action = "Index", Area = "Administration", IsAuthorize = false, Slag = "پنل", IsWidget = false, Parent = Guid.Empty, WidgetZoneName = "Header" });

            return model;
        }

        public void RegisterBundles(BundleCollection bundles)
        {
        }

        //public void RegisterBundles(BundleCollection bundles)
        //{
        //    var executingAssembly = Assembly.GetExecutingAssembly();

        //    // Mostly the default namespace and assembly name are the same
        //    var assemblyNameSpace = executingAssembly.GetName().Name;

        //    var scriptsBundle = new Bundle("~/bundles/AdminScript",
        //        new EmbeddedResourceTransform(new List<string>
        //        {
        //            assemblyNameSpace+".Scripts.jquery.1.10.0.min.js",
        //            assemblyNameSpace+".Scripts.jquery.ui.1.11.4.min.js",
        //            assemblyNameSpace +".Scripts.jquery.unobtrusive.ajax.js",

        //            assemblyNameSpace+".Scripts.Admin.plugins.charts.sparkline.min.js",
        //              assemblyNameSpace+ ".Scripts.Admin.plugins.forms.uniform.min.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.forms.select2.min.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.forms.inputmask.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.forms.autosize.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.forms.inputlimit.min.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.forms.listbox.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.forms.multiselect.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.forms.validate.min.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.forms.tags.min.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.forms.switch.min.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.forms.uploader.plupload.full.min.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.forms.uploader.plupload.queue.min.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.forms.wysihtml5.wysihtml5.min.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.forms.wysihtml5.toolbar.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.interface.daterangepicker.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.interface.fancybox.min.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.interface.moment.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.interface.jgrowl.min.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.interface.datatables.min.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.interface.colorpicker.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.interface.fullcalendar.min.js",
        //               assemblyNameSpace+".Scripts.Admin.plugins.interface.timepicker.min.js",
        //               assemblyNameSpace+".Scripts.Admin.bootstrap.min.js",
        //               assemblyNameSpace+".Scripts.Admin.application.js",
        //               assemblyNameSpace+".Scripts.PersianDatePicker.js",
        //              assemblyNameSpace+".Scripts.Admin.common.scripts.js"
        //        }, "application/javascript", executingAssembly));
        //    scriptsBundle = new Bundle("~/bundles/jqueryval",
        //       new EmbeddedResourceTransform(new List<string>
        //       {
        //            assemblyNameSpace+".Scripts.jquery.validate.js",
        //            assemblyNameSpace+".Scripts.jquery.validate.unobtrusive.min.js",
        //            assemblyNameSpace+".Scripts.Admin.jqueryval.default.js"
        //       }, "application/javascript", executingAssembly));

        //    if (!HttpContext.Current.IsDebuggingEnabled)
        //    {
        //        scriptsBundle.Transforms.Add(new JsMinify());
        //    }

        //    bundles.Add(scriptsBundle);

        //    var cssBundle = new Bundle("~/Content/AdminCss",
        //        new EmbeddedResourceTransform(new List<string>
        //        {
        //            //assemblyNameSpace + ".Content.prettify.css",
        //        assemblyNameSpace+".Content.bootstrap.min.css",
        //        assemblyNameSpace+".Content.londinium-theme.css",
        //        assemblyNameSpace+".Content.styles.css",
        //        assemblyNameSpace+".Content.icons.css",
        //        assemblyNameSpace+".Content.font-awesome.min.css",
        //        assemblyNameSpace+".Content.PersianDatePicker.css"
        //        }, "text/css", executingAssembly));

        //    if (!HttpContext.Current.IsDebuggingEnabled)
        //    {
        //        cssBundle.Transforms.Add(new CssMinify());
        //    }

        //    bundles.Add(cssBundle);

        //    BundleTable.EnableOptimizations = true;
        //}

        public void RegisterRoutes(RouteCollection routes)
        {
            //todo: add custom routes.

            var assembly = Assembly.GetExecutingAssembly();
            // Mostly the default namespace and assembly name are the same
            var nameSpace = assembly.GetName().Name;
            var resourcePath = string.Format("{0}.Images", nameSpace);

            routes.Insert(0,
                new Route("Administration/Images/{file}.{extension}",
                    new RouteValueDictionary(new { }),
                    new RouteValueDictionary(new { extension = "png|jpg" }),
                    new EmbeddedResourceRouteHandler(assembly, resourcePath, cacheDuration: TimeSpan.FromDays(30))
                ));
        }

        public void RegisterServices(StructureMap.IContainer container)
        {
        }
    }
}