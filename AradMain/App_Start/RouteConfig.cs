using System.Web.Mvc;
using System.Web.Routing;

namespace AradMain
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            #region IgnoreRoutes

            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.ico");
            routes.IgnoreRoute("{resource}.png");
            routes.IgnoreRoute("{resource}.jpg");
            routes.IgnoreRoute("{resource}.gif");
            routes.IgnoreRoute("{resource}.txt");

            #endregion IgnoreRoutes

            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                // مشخص كردن فضاي نام مرتبط جهت جلوگيري از تداخل با ساير قسمت‌هاي برنامه
                namespaces: new[] { string.Format("{0}.Controllers", typeof(RouteConfig).Namespace) }
            );
            routes.MapRoute(
             name: "Default2",
             url: "{controller}/{action}/{id}/{title}",
              defaults: new { controller = "Widget", action = "Index", id = UrlParameter.Optional, title = UrlParameter.Optional },
             // مشخص كردن فضاي نام مرتبط جهت جلوگيري از تداخل با ساير قسمت‌هاي برنامه
             namespaces: new[] { string.Format("{0}.Controllers", typeof(RouteConfig).Namespace) }
         );
        }
    }
}