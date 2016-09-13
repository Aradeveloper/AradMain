using BlogPlugin.Models;
using BlogPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlugin.IService
{
    public interface ICommentService
    {
        int Count { get; }

        void AddComment(PostComment comment);

        void Remove(int id);

        void Update(EditCommentModel comment);

        IList<PostComment> GetAll(Func<PostComment, bool> expression);

        IList<PostComment> GetAllComments();

        void Approve(int id);

        void DisApprove(int id);

        IList<CommentDataTableModel> GetDataTable(int Id);

        PostComment Find(int id);

        ICollection<PostComment> FindCommentsReply(int id);

        IList<PostComment> GetArticleComments(int articleId);

        IList<PostComment> GetArticleComments(int articleId, Func<PostComment, bool> expression);
        PostComment GetComment(int id);
        int approvedcoment(int id);

        int unapprovedcomment(int id);
    }
}