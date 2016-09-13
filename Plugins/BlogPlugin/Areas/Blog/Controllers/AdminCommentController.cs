using AradCms.Core.Context;
using AradCms.Core.Controllers;
using AradCms.Core.Filters;
using AradCms.Core.HtmlCleaner;
using AradCms.Core.IService;
using BlogPlugin.IService;
using BlogPlugin.ViewModel;
using BlogPlugin.Models;
using System.ComponentModel;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System;

namespace BlogPlugin.Areas.Blog.Controllers
{
    public partial class AdminCommentController : BaseController
    {
        private readonly IPermissionService _permissionService;

        private readonly IApplicationUserManager _userservice;
        private readonly ICommentService _commentService;
        private readonly IPostService _postservice;
        private readonly IUnitOfWork _uow;
        public static int ParentId = 0;
        public static int returnid = 0;
        public static int postid = 0;
        public AdminCommentController(IPermissionService permissionservice, IApplicationUserManager userservice, ICommentService commentService, IPostService postservice, IUnitOfWork uow)
        {
            _uow = uow;
            _commentService = commentService;
            _permissionService = permissionservice;
            _postservice = postservice;
            _userservice = userservice;
        }
        // GET: Blog/AdminComment
        public virtual ActionResult List(int id)
        {
            if (id < 0)
            {
                return RedirectToAction("Index", "Error", new { area = "" });
            }
            else
            {
                ViewBag.CatId = _postservice.Find(id).BlogId;
                postid = id;
                returnid = id;
                var model = _commentService.GetArticleComments(id);
                if (model == null)
                {
                    return RedirectToAction("Index", "Error", new { area = "" });
                }
                else
                {
                    return View(BMVC.Blog.AdminComment.Views.List, model);
                }
            }
        }

        #region Reply Comments

        [HttpGet]
        [AradAuthorize("CanViewReplyCommentList", AreaName = "Blog", IsMenu = false)]
        [DisplayName("مشاهده پاسخ دیدگاه")]
        
        public virtual ActionResult ReplyShow(int Id)
        {
            var model = _commentService.FindCommentsReply(Id);
            return PartialView(BMVC.Blog.AdminComment.Views.ReplyShow, model);
        }

        /// <returns></returns>
        [HttpGet]
        [DisplayName("ثبت پاسخ دیدگاه")]
        [AradAuthorize("CanReplyComment", AreaName = "Blog", IsMenu = false)]
        
        public virtual ActionResult Reply(int id)
        {
            ParentId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Reply(AddUserCommentModel usercomment)
        {
            PostComment comment = new PostComment
            {
                Author = User.Identity.Name,
                CreatedOn = DateTime.Now,
                ParentId = ParentId,
                Body = usercomment.Body,
                IsPublished = true,
                
                PostID = postid
            };
            _commentService.AddComment(comment);
            _uow.SaveAllChanges();
            return RedirectToAction(BMVC.Blog.AdminComment.ActionNames.List, BMVC.Blog.AdminComment.Name, new { Id = returnid });
        }

        #endregion Reply Comments

        #region Edit

        [HttpGet]
        [AradAuthorize("CanEditComment", AreaName = "Blog", IsMenu = false)]
        [DisplayName("ویرایش دیدگاه")]
        
        public virtual ActionResult Edit(int id)
        {
            PostComment selectedComment = _commentService.GetComment(id);
            return PartialView(BMVC.Blog.AdminComment.Views.Edit, new EditCommentModel
            {
                Id = selectedComment.Id,
                Body = selectedComment.Body
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(EditCommentModel commentModel)
        {
            _commentService.Update(commentModel);
            _uow.SaveAllChanges();
            return RedirectToAction(BMVC.Blog.AdminComment.ActionNames.List, BMVC.Blog.AdminComment.Name, new { Id = returnid });
        }

        #endregion Edit

        #region Approve Comments

        [HttpGet]
        public virtual ActionResult Approve(int id)
        {
            _commentService.Approve(id);
            _uow.SaveChanges();
            return RenderApproveButtons(id, "disapproved");
        }

        [HttpGet]
        public virtual ActionResult DisApprove(int id)
        {
            _commentService.DisApprove(id);
            _uow.SaveChanges();
            return RenderApproveButtons(id, "approved");
        }

        [ChildActionOnly]
        public virtual ActionResult RenderApproveButtons(int id, string type)
        {
            ViewBag.Id = id;
            ViewBag.Status = (type == "approved") ? "approved" : "disapproved";
            return PartialView(BMVC.Blog.AdminComment.Views._ApproveStatusButton);
        }

        #endregion Approve Comments

        #region Delete

        [HttpGet]
        [AradAuthorize("CanDeleteComment", AreaName = "Blog", IsMenu = false)]
        [DisplayName("حذف دیدگاه")]
        
        public virtual ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comment = _commentService.Find(id.Value);
            return View(comment);
        }

        // POST: Admin/Articles/Delete/5
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
                _commentService.Remove(id.Value);
                _uow.SaveAllChanges();
                return RedirectToAction(BMVC.Blog.AdminComment.ActionNames.List, BMVC.Blog.AdminComment.Name, new { Id = returnid });
            }
        }

        #endregion Delete
    }
}