using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.Filters;
using AradCms.Core.HtmlCleaner;
using AradCms.Core.IService;

using AradCms.Core.ViewModel.Site;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonInfoPlugin.Areas.SiteInfo.Controllers
{
    public partial class AdminSiteInfoController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly ISiteInfoService _infoservice;
        private readonly IFileService _fileservice;

        public AdminSiteInfoController(IUnitOfWork uow, ISiteInfoService infoservice, IFileService fileservice)
        {
            _uow = uow;
            _infoservice = infoservice;
            _fileservice = fileservice;
        }

        // GET: SiteInfo/AdminSiteInfo
        [HttpGet]
        [AradAuthorize(SiteInfoPermition.CanViewSiteInfo, AreaName = "SiteInfo", IsMenu = true)]
        [DisplayName("مشاهده اطلاعات وبسایت")]
        public virtual ActionResult SiteInfo()
        {
            var model = _infoservice.GetSiteDetail(1);
            return View(InfMVC.SiteInfo.AdminSiteInfo.Views.SiteInfo, model);
        }

        [HttpGet]
        [AradAuthorize(SiteInfoPermition.CanEditSiteInfo, AreaName = "SiteInfo", IsMenu = true)]
        [DisplayName("ویرایش اطلاعات سایت")]
        public virtual ActionResult Edit(int id)
        {
            var model = _infoservice.GetUpdateData(id);

            return View(InfMVC.SiteInfo.AdminSiteInfo.Views.Edit, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(AddOrUpdateSiteInfo model, HttpPostedFileBase InputFile)
        {
            string path = "~/Uploads/SiteMedia/Site/";

            if (InputFile != null)
            {
                path = _fileservice.Add(InputFile, path);
            }
            else
            {
                path = _infoservice.Find(model.Id).AboutImage;
            }
            var selectedmodel = new AddOrUpdateSiteInfo
            {
                SiteName = model.SiteName.ToSafeHtml(),

                AboutImage = path,

                Id = model.Id,

                EmailPassword = model.EmailPassword,
                EmailUsername = model.EmailUsername,
                GooglemapCode = model.GooglemapCode,

                SiteAboute = model.SiteAboute.ToSafeHtml(),
                SiteAddress = model.SiteAddress.ToSafeHtml(),
                SiteContact = model.SiteContact.ToSafeHtml(),
                SitePhone = model.SitePhone,
                Smtpserver = model.Smtpserver
            };
            _infoservice.Update(selectedmodel);
            _uow.SaveAllChanges();
            return RedirectToAction(InfMVC.SiteInfo.AdminSiteInfo.ActionNames.SiteInfo, InfMVC.SiteInfo.AdminSiteInfo.Name);
        }
    }
}