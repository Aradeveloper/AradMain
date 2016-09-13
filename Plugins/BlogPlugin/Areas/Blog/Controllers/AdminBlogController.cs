using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.Filters;
using AradCms.Core.HtmlCleaner;
using AradCms.Core.IService;
using BlogPlugin.IService;
using BlogPlugin.ViewModel;
using System.ComponentModel;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BlogPlugin.Areas.Blog.Controllers
{
    public partial class AdminBlogController : BaseController
    {
        private readonly IBlogService _blogservice;
        private readonly IFileService _fileservice;

        private readonly IUnitOfWork _uow;

        public AdminBlogController(IBlogService blogService, IUnitOfWork uow, IFileService fileservice)
        {
            _blogservice = blogService;
            _uow = uow;
            _fileservice = fileservice;
        }

        // GET: Blog/Admin
        [HttpGet]
        [AradAuthorize("CanViewBlogItemList", AreaName = "Blog", IsMenu = true)]
        [DisplayName("مشاهده لیست بلاگ ها")]
        public virtual ActionResult List()
        {
            var model = _blogservice.GetBlogs();

            return View(BMVC.Blog.AdminBlog.Views.List, model);
        }

        [HttpGet]
        [AradAuthorize("CanCreateBlogItemList", AreaName = "Blog", IsMenu = false)]
        [DisplayName("ایجاد بلاگ")]
        public virtual ActionResult Create()
        {
            return View(BMVC.Blog.AdminBlog.Views.Create);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(AddOrUpdateBlog model, HttpPostedFileBase InputFile)
        {
            if (InputFile != null)
            {
                string path = "~/Uploads/SiteMedia/Blog/";

                path = _fileservice.Add(InputFile, path);

                _blogservice.Add(new AddOrUpdateBlog
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
                return RedirectToAction(BMVC.Blog.AdminBlog.Views.Create, BMVC.Blog.AdminBlog.Name);
            }
            return RedirectToAction(BMVC.Blog.AdminBlog.ActionNames.List, BMVC.Blog.AdminBlog.Name);
        }

        [HttpGet]
        [AradAuthorize("CanEditBlogItemList", AreaName = "Blog", IsMenu = false)]
        [DisplayName("ویرایش بلاگ")]
        public virtual ActionResult Edit(int id)
        {
            var model = _blogservice.GetUpdateData(id);

            return View(BMVC.Blog.AdminBlog.Views.Edit, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(AddOrUpdateBlog model, HttpPostedFileBase InputFile)
        {
            string path = "~/Uploads/SiteMedia/Blog/";

            if (InputFile != null)
            {
                path = _fileservice.Add(InputFile, path);
            }
            else
            {
                path = _blogservice.Find(model.Id).ImagePath;
            }
            var selectedmodel = new AddOrUpdateBlog
            {
                Name = model.Name.ToSafeHtml(),

                BlogImage = path,

                Id = model.Id,

                IsActive = model.IsActive,
                Slug = model.Slug
            };
            _blogservice.Update(selectedmodel);
            _uow.SaveAllChanges();
            return RedirectToAction(BMVC.Blog.AdminBlog.ActionNames.List, BMVC.Blog.AdminBlog.Name);
        }

        [HttpGet]
        [AradAuthorize("CanDeleteBlogItemList", AreaName = "Blog", IsMenu = false)]
        [DisplayName("حذف بلاگ")]
        public virtual ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _blogservice.Find(id.Value);
            return PartialView(BMVC.Blog.AdminBlog.Views.Delete, model);
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
                _blogservice.Remove(id.Value);
                _uow.SaveAllChanges();
                return RedirectToAction(BMVC.Blog.AdminBlog.ActionNames.List, BMVC.Blog.AdminBlog.Name);
            }
        }
    }
}