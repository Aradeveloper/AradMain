using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.HtmlCleaner;
using BlogPlugin.IService;
using BlogPlugin.Models;
using BlogPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPlugin.Areas.Blog.Controllers
{
    public partial class WidgetController : Controller
    {
        // GET: Blog/Widget
        private readonly IPostService _postservice;

        private readonly IBlogService _blog;
        private readonly ICommentService _commentservice;
        private readonly IUnitOfWork _uow;
        public static int postid = 0;

        public WidgetController(IBlogService blog, IPostService postService, IUnitOfWork uow, ICommentService commentservice)
        {
            _blog = blog;
            _postservice = postService;
            _commentservice = commentservice;
            _uow = uow;
        }

        #region WidgetPart

        [HttpGet]
        public virtual ActionResult GetLatestPost()
        {
            var model = _postservice.GetLatestPost();
            return PartialView(BMVC.Blog.Widget.Views.GetLatestPost, model);
        }

        [HttpGet]
        public virtual ActionResult GetBlogList()
        {
            var model = _postservice.GetVisiblePosts(1);
            return PartialView(BMVC.Blog.Widget.Views.GetBlogList, model);
        }

        [HttpGet]
        public virtual ActionResult GetNewsList()
        {
            var model = _postservice.GetVisiblePosts(2);
            return PartialView(BMVC.Blog.Widget.Views.GetNewsList, model);
        }

        public virtual ActionResult GetServiceList()
        {
            var model = _postservice.GetVisiblePosts(4);
            return PartialView(BMVC.Blog.Widget.Views.GetNewsList, model);
        }

        [HttpGet]
        public virtual ActionResult GetBlogFooter()
        {
            var model = _postservice.GetVisiblePosts(3);
            return PartialView(BMVC.Blog.Widget.Views.GetBlogFooter, model);
        }

        [HttpGet]
        public virtual ActionResult Detailes(int Id)
        {
            var model = _postservice.GetPostDetail(Id);
            _postservice.IncrementVisitedCount(Id);
            _uow.SaveAllChanges();
            return View(BMVC.Blog.Widget.Views.Detailes, model);
        }

        [HttpGet]
        public virtual ActionResult GetPopullore()
        {
            var model = _postservice.GetPopullorePosts();
            return PartialView(BMVC.Blog.Widget.Views.GetPopullore, model);
        }

        [HttpGet]
        public virtual ActionResult GetList(int Id)
        {
            var model = _postservice.GetVisiblePosts(Id);
            return View(BMVC.Blog.Widget.Views.GetList, model);
        }

        public virtual ActionResult GetFooterNews()
        {
            var model = _postservice.GetPopullorePosts();
            return PartialView(BMVC.Blog.Widget.Views.GetFooterNews, model);
        }

        public virtual ActionResult GetComments(int id)
        {
            var model = _commentservice.GetArticleComments(id);
            return PartialView(BMVC.Blog.Widget.Views.GetComments, model);
        }

        [HttpGet]
        public virtual ActionResult AddComent(int id)
        {
            var model = _postservice.Find(id);

            postid = id;
            return PartialView(BMVC.Blog.Widget.Views.AddComent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult AddComent(PostComment model)
        {
            model.PostID = postid;
            model.Body = model.Body.ToSafeHtml();
            model.Email = model.Email.ToSafeHtml();
            model.Author = model.Author.ToSafeHtml();
            model.CreatedOn = DateTime.Now;
            model.IsPublished = false;
            _commentservice.AddComment(model);

            _uow.SaveAllChanges();
            return RedirectToAction(BMVC.Blog.Widget.ActionNames.Detailes, BMVC.Blog.Widget.Name, new { Id = postid, area = BMVC.Blog.Name });
        }

        [HttpGet]
        public virtual ActionResult GetBlogSidebar()
        {
            var model = _blog.GetBlogs();
            return PartialView(BMVC.Blog.Widget.Views.GetBlogSidebar, model);
        }

        #endregion WidgetPart
    }
}