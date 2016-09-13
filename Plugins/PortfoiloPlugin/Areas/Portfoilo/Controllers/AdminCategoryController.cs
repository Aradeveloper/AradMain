using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.Filters;
using AradCms.Core.HtmlCleaner;

using AradCms.Core.IService;
using PortfoiloPlugin.IService;
using PortfoiloPlugin.Models;
using PortfoiloPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PortfoiloPlugin.Areas.Portfoilo.Controllers
{
    public partial class AdminCategoryController : BaseController
    {
        private readonly ICategoryService _categoryservice;
        private readonly IFileService _fileservice;

        private readonly IUnitOfWork _uow;

        public AdminCategoryController(ICategoryService blogService, IUnitOfWork uow, IFileService fileservice)
        {
            _categoryservice = blogService;
            _uow = uow;
            _fileservice = fileservice;
        }

        // GET: Blog/Admin
        [HttpGet]
        [AradAuthorize("CanViewCategoryList", AreaName = "Portfoilo", IsMenu = true)]
        [DisplayName("مشاهده لیست بلاگ ها")]
        public virtual ActionResult List()
        {
            var model = _categoryservice.GetBlogs();

            return View(PoMVC.Portfoilo.AdminCategory.Views.List, model);
        }

        [HttpGet]
        [AradAuthorize("CanCreateCategory", AreaName = "Portfoilo", IsMenu = false)]
        [DisplayName("ایجاد مجموعه نمونه کار")]
        public virtual ActionResult Create()
        {
            return View(PoMVC.Portfoilo.AdminCategory.Views.Create);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(AddOrUpdateCategory model, HttpPostedFileBase InputFile)
        {
            if (InputFile != null)
            {
                string path = "~/Uploads/SiteMedia/Category/";

                path = _fileservice.Add(InputFile, path);

                _categoryservice.Add(new AddOrUpdateCategory
                {
                    Name = model.Name.ToSafeHtml(),
                    IsActive = model.IsActive,

                    BlogImage = path,

                    Slug = model.Slug
                });
                _uow.SaveAllChanges();
            }
            else if (InputFile == null)
            {
                ToastrError("لطفا فایل را انتخاب نمایید");
                return RedirectToAction(PoMVC.Portfoilo.AdminCategory.Views.Create, PoMVC.Portfoilo.AdminCategory.Name);
            }
            return RedirectToAction(PoMVC.Portfoilo.AdminCategory.ActionNames.List, PoMVC.Portfoilo.AdminCategory.Name);
        }

        [HttpGet]
        [AradAuthorize("CanEditCategory", AreaName = "Portfoilo", IsMenu = false)]
        [DisplayName("ویرایش مجموعه نمونه کار")]
        public virtual ActionResult Edit(int id)
        {
            var model = _categoryservice.GetUpdateData(id);

            return View(PoMVC.Portfoilo.AdminCategory.Views.Edit, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(AddOrUpdateCategory model, HttpPostedFileBase InputFile)
        {
            string path = "~/Uploads/SiteMedia/Category/";

            if (InputFile != null)
            {
                path = _fileservice.Add(InputFile, path);
            }
            else
            {
                path = _categoryservice.Find(model.Id).ImagePath;
            }
            var selectedmodel = new AddOrUpdateCategory
            {
                Name = model.Name.ToSafeHtml(),

                BlogImage = path,

                Id = model.Id,

                IsActive = model.IsActive,
                Slug = model.Slug
            };
            _categoryservice.Update(selectedmodel);
            _uow.SaveAllChanges();
            return RedirectToAction(PoMVC.Portfoilo.AdminCategory.ActionNames.List, PoMVC.Portfoilo.AdminCategory.Name);
        }

        [HttpGet]
        [AradAuthorize("CanDeleteCategory", AreaName = "Portfoilo", IsMenu = false)]
        [DisplayName("حذف مجموعه نمونه کار")]
        public virtual ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _categoryservice.Find(id.Value);
            return PartialView(PoMVC.Portfoilo.AdminCategory.Views.Delete, model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _categoryservice.Remove(id.Value);
                _uow.SaveAllChanges();
                return RedirectToAction(PoMVC.Portfoilo.AdminCategory.ActionNames.List, PoMVC.Portfoilo.AdminCategory.Name);
            }
        }
    }
}