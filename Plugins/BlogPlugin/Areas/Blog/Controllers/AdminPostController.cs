using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.Filters;
using AradCms.Core.HtmlCleaner;
using AradCms.Core.IService;
using BlogPlugin.IService;
using BlogPlugin.Models;
using BlogPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BlogPlugin.Areas.Blog.Controllers
{
    public partial class AdminPostController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IPostService _postservice;
        private readonly IFileService _fileservice;
        public static int IdBlog = 0;

        public AdminPostController(IUnitOfWork unitOfWork, IPostService postservice, IFileService fileservice)
        {
            _uow = unitOfWork;
            _postservice = postservice;
            _fileservice = fileservice;
        }

        // GET: Blog/AdminPost
        [HttpGet]
        [AradAuthorize("CanViewPostItemList", AreaName = "Blog", IsMenu = true)]
        [DisplayName("مشاهده لیست پست ها")]
        public virtual ActionResult List(int Id)
        {
            IdBlog = Id;
            ViewBag.BlogId = Id;
            var model = _postservice.GetPosts(Id);
            return View(BMVC.Blog.AdminPost.Views.List, model);
        }

        [HttpGet]
        [AradAuthorize("CanCreatePostItem", AreaName = "Blog", IsMenu = true)]
        [DisplayName("ایجاد پست جدید")]
        public virtual ActionResult Create(int Id)
        {
            IdBlog = Id;
            return View(BMVC.Blog.AdminPost.Views.Create);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(AddOrUpdatePost model, HttpPostedFileBase InputFile)
        {
            if (InputFile != null)
            {
                string path = "~/Uploads/SiteMedia/Post/";

                path = _fileservice.Add(InputFile, path);
                _postservice.Add(new AddOrUpdatePost
                {
                    Summary = model.Summary.ToSafeHtml(),
                    Published = model.Published,
                    BlogId = IdBlog,
                    Body = model.Body.ToSafeHtml(),
                    Title = model.Title,
                    UserName = User.Identity.Name,
                    Slug = model.Slug,
                    PostImage = path,
                    Tags = model.Tags,
                    CommentStatuse = model.CommentStatuse
                });

                _uow.SaveAllChanges();
            }
            else if (InputFile == null)
            {
                ToastrError("لطفا فایل را انتخاب نمایید");
                return RedirectToAction(BMVC.Blog.AdminPost.Views.Create, BMVC.Blog.AdminPost.Name, model);
            }

            return RedirectToAction(BMVC.Blog.AdminPost.ActionNames.List, BMVC.Blog.AdminPost.Name, new { Id = IdBlog });
        }

        [HttpGet]
        [AradAuthorize("CanEditPostItem", AreaName = "Blog", IsMenu = true)]
        [DisplayName("ویرایش پست")]
        public virtual ActionResult Edit(int id)
        {
            var model = _postservice.GetUpdateData(id);
            IdBlog = model.BlogId;
            return View(BMVC.Blog.AdminPost.Views.Edit, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(AddOrUpdatePost model, HttpPostedFileBase InputFile)
        {
            string path = "~/Uploads/SiteMedia/Blog/";

            if (InputFile != null)
            {
                path = _fileservice.Add(InputFile, path);
            }
            else
            {
                path = _postservice.Find(model.Id).ImagePath;
            }
            var selectedmodel = new AddOrUpdatePost
            {
                Id = model.Id,
                Summary = model.Summary.ToSafeHtml(),
                Published = model.Published,
                BlogId = IdBlog,
                Body = model.Body.ToSafeHtml(),
                Title = model.Title,
                UserName = User.Identity.Name,
                Slug = model.Slug,
                PostImage = path,
                Tags = model.Tags,
                CommentStatuse = model.CommentStatuse,
                CreateTime = model.CreateTime,
                EditTime = model.EditTime
            };
            _postservice.Update(selectedmodel);
            _uow.SaveChanges();
            return RedirectToAction(BMVC.Blog.AdminPost.ActionNames.List, BMVC.Blog.AdminPost.Name, new { Id = selectedmodel.BlogId });
        }

        [HttpGet]
        [AradAuthorize("CanDeletePostItem", AreaName = "Blog", IsMenu = true)]
        [DisplayName("حذف پست")]
        public virtual ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _postservice.Find(id.Value);
            return PartialView(BMVC.Blog.AdminPost.Views.Delete, model);
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
                return RedirectToAction(BMVC.Blog.AdminPost.ActionNames.List, BMVC.Blog.AdminPost.Name, new { Id = IdBlog });
            }
        }

        [HttpPost]
        [AjaxOnly]
        // [CheckReferrer]
        [AradAuthorize("")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult TagNameExist(string name)
        {
            return _postservice.ChechForExisByName(name) ? Json(false) : Json(true);
        }
    }
}