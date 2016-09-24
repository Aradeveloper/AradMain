using AradCms.Core.Common;
using AradCms.Core.Helpers;
using StackExchange.Profiling;
using StructureMap.Web.Pipeline;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI;

namespace AradMain
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            HttpContext.Current.Response.Headers.Remove("Server");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Implement HTTP compression
            HttpApplication app = (HttpApplication)sender;

            // Retrieve accepted encodings
            string encodings = app.Request.Headers.Get("Accept-Encoding");
            if (encodings != null)
            {
                // Check the browser accepts deflate or gzip (deflate takes preference)
                encodings = encodings.ToLower();
                if (encodings.Contains("deflate"))
                {
                    app.Response.Filter = new DeflateStream(app.Response.Filter, CompressionMode.Compress);
                    app.Response.AppendHeader("Content-Encoding", "deflate");
                }
                else if (encodings.Contains("gzip"))
                {
                    app.Response.Filter = new GZipStream(app.Response.Filter, CompressionMode.Compress);
                    app.Response.AppendHeader("Content-Encoding", "gzip");
                }
            }
        }

        #region Application_Start

        protected void Application_Start()
        {
            try
            {
                AreaRegistration.RegisterAllAreas();
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                CustomAppConfig.Config();
                ViewEngines.Engines.Clear();
                ViewEngines.Engines.Add(new RazorViewEngine());
                MvcHandler.DisableMvcResponseHeader = true;
                ModelBinders.Binders.Add(typeof(DateTime), new PersianDateModelBinder());
                ApplicationVerification.Check();
            }
            catch
            {
                HttpRuntime.UnloadAppDomain(); // سبب ری استارت برنامه و آغاز مجدد آن با درخواست بعدی می‌شود
                throw;
            }
        }

        #endregion Application_Start

        #region Application_EndRequest

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContextLifecycle.DisposeAndClearAll();
            MiniProfiler.Stop();
        }

        #endregion Application_EndRequest

        #region ShouldIgnoreRequest

        private bool ShouldIgnoreRequest()
        {
            string[] reservedPath =
              {
                  "/__browserLink",
                  "/Scripts",
                  "/Content"
              };

            var rawUrl = Context.Request.RawUrl;
            if (reservedPath.Any(path => rawUrl.StartsWith(path, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            return BundleTable.Bundles.Select(bundle => bundle.Path.TrimStart('~'))
                      .Any(bundlePath => rawUrl.StartsWith(bundlePath, StringComparison.OrdinalIgnoreCase));
        }

        #endregion ShouldIgnoreRequest

        #region Application_AuthenticateRequest

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            //if (ShouldIgnoreRequest())
            //    return;
            //if (Context.User == null)
            //    return;
            //if (!Context.User.Identity.IsAuthenticated)
            //    return;

            //var userId = Context.User.Identity.GetUserId<int>();
            //var authenticationManager = ProjectObjectFactory.Container.GetInstance<IAuthenticationManager>();
            //var userManager = ProjectObjectFactory.Container.GetInstance<IApplicationUserManager>();

            //if (userManager.CheckIsUserBannedOrDelete(userId))
            //{
            //    authenticationManager.SignOut();
            //    return;
            //}

            //if (!userManager.IsModifiedRolesOrPermissions(userId)) return;

            //userManager.SetFalseModifyRolesOrPermissions(userId);
            //var permissions = userManager.GetUserPermissions(userId);
            //SetPermissions(permissions);
        }

        #endregion Application_AuthenticateRequest

        #region Private

        private void SetPermissions(IEnumerable<string> permissions)
        {
            Context.User =
                new GenericPrincipal(Context.User.Identity, permissions.ToArray());
        }

        #endregion Private

        #region ErrorHandle

        private void Application_Error(Object sender, EventArgs e)
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                RequestContext requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;
                /* when the request is ajax the system can automatically handle a mistake with a JSON response. then overwrites the default response */
                if (requestContext.HttpContext.Request.IsAjaxRequest())
                {
                    httpContext.Response.Clear();
                    string controllerName = requestContext.RouteData.GetRequiredString("controller");
                    IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
                    IController controller = factory.CreateController(requestContext, controllerName);
                    ControllerContext controllerContext = new ControllerContext(requestContext, (ControllerBase)controller);

                    JsonResult jsonResult = new JsonResult();
                    jsonResult.Data = new { success = false, serverError = "500" };
                    jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    jsonResult.ExecuteResult(controllerContext);
                    httpContext.Response.End();
                }
                else
                {
                    httpContext.Response.Redirect("~/Error");
                }
            }
        }

        #endregion ErrorHandle

        #region Culture

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom == "culture") // culture name (e.g. "en-US") is what should vary caching
            {
                string cultureName = null;

                // Attempt to read the culture cookie from Request
                HttpCookie cultureCookie = Request.Cookies["_culture"];
                if (cultureCookie != null)
                {
                    cultureName = cultureCookie.Value;
                }
                else
                {
                    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages
                }

                // Validate culture name
                cultureName = CultureHelper.GetImplementedCulture(cultureName);

                return cultureName.ToLower(); // use culture name as the cache key, "es", "en-us", "es-cl", etc.
            }

            return base.GetVaryByCustomString(context, custom);
        }

        #endregion Culture
    }
}