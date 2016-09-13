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
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PortfoiloPlugin.Areas.Portfoilo.Controllers
{
    public partial class AdminPortfoiloController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IPortfoiloService _postservice;
        private readonly IFileService _fileservice;
        public static int IdBlog = 0;

        public AdminPortfoiloController(IUnitOfWork unitOfWork, IPortfoiloService postservice, IFileService fileservice)
        {
            _uow = unitOfWork;
            _postservice = postservice;
            _fileservice = fileservice;
        }

        // GET: Blog/AdminPost
        [HttpGet]
        [AradAuthorize("CanViewPortfoiloList", AreaName = "Portfoilo", IsMenu = true)]
        [DisplayName("مشاهده پورتفویلو ها")]
        public virtual ActionResult List(int Id)
        {
            IdBlog = Id;
            var model = _postservice.GetPosts(Id);
            return View(PoMVC.Portfoilo.AdminPortfoilo.Views.List, model);
        }

        [HttpGet]
        [AradAuthorize("CanCreatePortfoilo", AreaName = "Portfoilo", IsMenu = true)]
        [DisplayName("ایجاد پورتفویلو")]
        public virtual ActionResult Create(int Id)
        {
            IdBlog = Id;
            return View(PoMVC.Portfoilo.AdminPortfoilo.Views.Create);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(AddOrUpdatePortfoilo model, HttpPostedFileBase InputFile)
        {
            if (InputFile != null)
            {
                string path = "~/Uploads/SiteMedia/Portfoilo/";

                path = _fileservice.Add(InputFile, path);
                _postservice.Add(new AddOrUpdatePortfoilo
                {
                    Summary = model.Summary.ToSafeHtml(),
                    Published = model.Published,
                    BlogId = IdBlog,
                    Body = model.Body.ToSafeHtml(),
                    Title = model.Title,
                    UserName = User.Identity.Name,
                    Slug = model.Slug,
                    PostImage = path
                });

                _uow.SaveAllChanges();
            }
            else if (InputFile == null)
            {
                ToastrError("لطفا فایل را انتخاب نمایید");
                return RedirectToAction(PoMVC.Portfoilo.AdminPortfoilo.Views.Create, PoMVC.Portfoilo.AdminPortfoilo.Name, model);
            }

            return RedirectToAction(PoMVC.Portfoilo.AdminPortfoilo.ActionNames.List, PoMVC.Portfoilo.AdminPortfoilo.Name, new { Id = IdBlog });
        }

        [HttpGet]
        [AradAuthorize("CanEditPortfoilo", AreaName = "Portfoilo", IsMenu = true)]
        [DisplayName("ویرایش پورتفویلو")]
        public virtual ActionResult Edit(int id)
        {
            var model = _postservice.GetUpdateData(id);
            return View(PoMVC.Portfoilo.AdminPortfoilo.Views.Edit, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(AddOrUpdatePortfoilo model, HttpPostedFileBase InputFile)
        {
            string path = "~/Uploads/SiteMedia/Portfoilo/";

            if (InputFile != null)
            {
                path = _fileservice.Add(InputFile, path);
            }
            else
            {
                path = _postservice.Find(model.Id).ImagePath;
            }
            var selectedmodel = new AddOrUpdatePortfoilo
            {
                Id = model.Id,
                Summary = model.Summary.ToSafeHtml(),
                Published = model.Published,
                BlogId = model.BlogId,
                Body = model.Body.ToSafeHtml(),
                Title = model.Title,
                UserName = User.Identity.Name,
                Slug = model.Slug,
                PostImage = path
            };
            _postservice.Update(selectedmodel);
            _uow.SaveChanges();
            return RedirectToAction(PoMVC.Portfoilo.AdminPortfoilo.ActionNames.List, PoMVC.Portfoilo.AdminPortfoilo.Name, new { Id = IdBlog });
        }

        [HttpGet]
        [AradAuthorize("CanDeletePortFoilo", AreaName = "Portfoilo", IsMenu = true)]
        [DisplayName("حذف پورتفویلو")]
        public virtual ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _postservice.Find(id.Value);
            return PartialView(PoMVC.Portfoilo.AdminPortfoilo.Views.Delete, model);
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
                _postservice.Remove(id.Value);
                _uow.SaveAllChanges();
                return RedirectToAction(PoMVC.Portfoilo.AdminPortfoilo.ActionNames.List, PoMVC.Portfoilo.AdminPortfoilo.Name, new { Id = IdBlog });
            }
        }
    }
}