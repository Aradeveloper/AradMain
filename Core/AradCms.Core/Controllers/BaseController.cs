using AradCms.Core.Controllers.Alerts;
using AradCms.Core.Helpers;
using System;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace AradCms.Core.Controllers
{
    public class BaseController : Controller
    {
        #region bootstrapalerts

        public void BoostrapSuccess(string message, bool dismissable = false)
        {
            var bootstrapMessage = new BootstrapMessage
            {
                AlertType = AlertType.Success,
                Dismissable = dismissable,
                Message = message
            };
            this.AddBootstrapAlert(bootstrapMessage);
        }

        public void BoostrapInformation(string message, bool dismissable = false)
        {
            var bootstrapMessage = new BootstrapMessage
            {
                AlertType = AlertType.Information,
                Dismissable = dismissable,
                Message = message
            };
            this.AddBootstrapAlert(bootstrapMessage);
        }

        public void BoostrapWarning(string message, bool dismissable = false)
        {
            var bootstrapMessage = new BootstrapMessage
            {
                AlertType = AlertType.Warning,
                Dismissable = dismissable,
                Message = message
            };
            this.AddBootstrapAlert(bootstrapMessage);
        }

        public void BoostrapDanger(string message, bool dismissable = false)
        {
            var bootstrapMessage = new BootstrapMessage
            {
                AlertType = AlertType.Danger,
                Dismissable = dismissable,
                Message = message
            };
            this.AddBootstrapAlert(bootstrapMessage);
        }

        #endregion bootstrapalerts

        #region toastralerts

        public void ToastrError(string message, string title = "",
            bool isSticky = false)
        {
            var toastMessage = new ToastMessage
            {
                AlertType = AlertType.Error,
                IsSticky = isSticky,
                Message = message,
                Title = title
            };

            this.AddToastrAlert(toastMessage);
        }

        public void ToastrWarning(string message, string title = "",
          bool isSticky = false)
        {
            var toastMessage = new ToastMessage
            {
                AlertType = AlertType.Warning,
                IsSticky = isSticky,
                Message = message,
                Title = title
            };
            this.AddToastrAlert(toastMessage);
        }

        public void ToastrInformation(string message, string title = "",
          bool isSticky = false)
        {
            var toastMessage = new ToastMessage
            {
                AlertType = AlertType.Information,
                IsSticky = isSticky,
                Message = message,
                Title = title
            };
            this.AddToastrAlert(toastMessage);
        }

        public void ToastrSuccess(string message, string title = "",
          bool isSticky = false)
        {
            var toastMessage = new ToastMessage
            {
                AlertType = AlertType.Success,
                IsSticky = isSticky,
                Message = message,
                Title = title
            };
            this.AddToastrAlert(toastMessage);
        }

        #endregion toastralerts

        #region Validation

        [ChildActionOnly]
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        #endregion Validation

        #region Culture

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        #endregion Culture
    }
}