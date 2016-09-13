using Microsoft.Owin.Security;
using System;
using System.Net;
using System.Web.Mvc;

namespace AradCms.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class AradAuthorizeAttribute : AuthorizeAttribute
    {
        #region Ctor

        public AradAuthorizeAttribute(params string[] permissions) : base()
        {
            Roles = string.Join(",", permissions);
        }

        #endregion Ctor

        #region Properties

        public string AreaName { get; set; }
        public bool IsMenu { get; set; }
        public IAuthenticationManager AuthenticationManager { get; set; }

        #endregion Properties

        #region HandleUnauthorizedRequest

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                AuthenticationManager.SignOut();
                throw new UnauthorizedAccessException(); //to avoid multiple redirects
            }
            else
            {
                HandleAjaxRequest(filterContext);
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        #endregion HandleUnauthorizedRequest

        #region Private

        private static void HandleAjaxRequest(ControllerContext filterContext)
        {
            var ctx = filterContext.HttpContext;
            if (!ctx.Request.IsAjaxRequest())
                return;

            ctx.Response.StatusCode = (int)HttpStatusCode.Forbidden; //براي درخواست‌هاي اجكسي اعتبار سنجي نشده
            ctx.Response.End();
        }

        #endregion Private
    }
}