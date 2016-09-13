using AradCms.Core.Controllers;
using AradCms.Core.Extentions;
using AradCms.Core.Filters;
using AradCms.Core.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProductsPlugin
{
    public static class SystemConfiguration
    {
        #region PermissionConfiguration

        public static IEnumerable<ApplicationPermission> ConfigPermissions()
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

        #endregion PermissionConfiguration
    }
}