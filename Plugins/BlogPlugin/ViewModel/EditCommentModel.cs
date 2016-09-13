using System.Web.Mvc;

namespace BlogPlugin.ViewModel
{
    public class EditCommentModel
    {
        public int Id { get; set; }

        [AllowHtml]
        public string Body { get; set; }
    }
}