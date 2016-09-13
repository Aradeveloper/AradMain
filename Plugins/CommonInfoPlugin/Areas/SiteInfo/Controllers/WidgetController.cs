using AradCms.Core.HtmlCleaner;
using AradCms.Core.IService;
using AradCms.Core.ViewModel.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CommonInfoPlugin.Areas.SiteInfo.Controllers
{
    public partial class WidgetController : Controller
    {
        private readonly ISiteInfoService _siteinfo;
        private readonly ISiteEmailService _emailservice;

        public WidgetController(ISiteInfoService siteinfo, ISiteEmailService emailservice)
        {
            _siteinfo = siteinfo;
            _emailservice = emailservice;
        }

        // GET: SiteInfo/Widget
        public virtual ActionResult GetHeader()
        {
            var model = _siteinfo.GetSiteDetail(1);
            return PartialView(InfMVC.SiteInfo.Widget.Views.GetHeader, model);
        }

        public virtual ActionResult GetSiteName()
        {
            var model = _siteinfo.GetSiteDetail(1);
            return PartialView(InfMVC.SiteInfo.Widget.Views.GetSiteName, model);
        }

        [HttpGet]
        public virtual ActionResult Contactus()
        {
            var model = _siteinfo.GetSiteDetail(1);
            return View(InfMVC.SiteInfo.Widget.Views.Contactus, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> SendMail(EmailForm model)
        {
            model.EmailReciver = null;
            bool x = await _emailservice.SendEmail(model);
            if (x)
            {
                return RedirectToAction(InfMVC.SiteInfo.Widget.ActionNames.Contactus, InfMVC.SiteInfo.Widget.Name, new { area = InfMVC.SiteInfo.Name });
            }
            else
            {
                return RedirectToAction(InfMVC.SiteInfo.Widget.ActionNames.Contactus, InfMVC.SiteInfo.Widget.Name);
            }
        }

        public virtual ActionResult Contact()
        {
            return PartialView(InfMVC.SiteInfo.Widget.Views.Contact);
        }

        public virtual ActionResult FooterAboute()
        {
            var model = _siteinfo.GetSiteDetail(1);
            return PartialView(InfMVC.SiteInfo.Widget.Views.FooterAboute, model);
        }

        public virtual ActionResult ContactFooter()
        {
            var model = _siteinfo.GetSiteDetail(1);
            return PartialView(InfMVC.SiteInfo.Widget.Views.ContactFooter, model);
        }
    }
}