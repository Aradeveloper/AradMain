using AradCms.Core.Context;
using BlogPlugin.IService;
using BlogPlugin.Models;
using BlogPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogPlugin.Service
{
    public class ComentService : ICommentService
    {
        private readonly IDbSet<PostComment> _postcomment;
        private readonly IUnitOfWork _uow;

        public ComentService(IUnitOfWork uow)
        {
            _uow = uow;
            _postcomment = _uow.Set<PostComment>();
        }

        public int Count
        {
            get
            {
                return _postcomment.Count();
            }
        }

        public void AddComment(PostComment comment)
        {
            _postcomment.Add(comment);
        }

        public void Approve(int id)
        {
            _postcomment.Find(id).IsPublished = true;
        }

        public void DisApprove(int id)
        {
            _postcomment.Find(id).IsPublished = false;
        }

        public PostComment Find(int id)
        {
            return _postcomment.Find(id);
        }

        public ICollection<PostComment> FindCommentsReply(int id)
        {
            ICollection<PostComment> returncoment = _postcomment.Where(a => a.ParentId == id).ToList();

            return (returncoment);
        }

        public IList<PostComment> GetAll(Func<PostComment, bool> expression)
        {
            return _postcomment.Where(expression).ToList();
        }

        public IList<PostComment> GetAllComments()
        {
            return _postcomment.ToList();
        }

        public IList<PostComment> GetArticleComments(int articleId)
        {
            return _postcomment.Where(comment => comment.Post.Id == articleId)
                .Include(comment => comment.Childeren)
                .OrderBy(comment => comment.CreatedOn)
                .ToList().Where(comment => comment.Parent == null).ToList();
        }

        public IList<PostComment> GetArticleComments(int articleId, Func<PostComment, bool> expression)
        {
            return _postcomment.Where(comment => comment.Post.Id == articleId)
                .Include(comment => comment.Childeren)

                .Where(expression)
                .OrderBy(comment => comment.CreatedOn)
                .ToList().Where(comment => comment.Parent == null).ToList();
        }

        public IList<CommentDataTableModel> GetDataTable(int Id)
        {
            if (Count > 0)
            {
                var model = (from a in _postcomment
                             select new CommentDataTableModel
                             {
                                 ID = a.Id,
                                 Author = a.Author,

                                 Body = a.Body,

                                 IsPublished = a.IsPublished,
                                 CreatedOn = a.CreatedOn,
                                 Email = a.Email,
                                 PostID = a.PostID.Value
                             }).Where(b => b.PostID == Id).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public void Remove(int id)
        {
            var selectedComment = _postcomment.Find(id);

            _postcomment.Remove(selectedComment);
        }

        //approved cooment count
        public int approvedcoment(int id)
        {
            return _postcomment.Where(a => a.PostID == id && a.IsPublished == true).Count();
        }

        public int unapprovedcomment(int id)
        {
            return _postcomment.Where(a => a.PostID == id && a.IsPublished == false).Count();
        }

        public void Update(EditCommentModel comment)
        {
            var model = _postcomment.Find(comment.Id);
            
            model.Body = comment.Body;
            
        }

        public PostComment GetComment(int id)
        {
            return _postcomment.Find(id);
        }
    }
}