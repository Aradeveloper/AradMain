using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPlugin.ViewModel
{
    public class CommentDataTableModel
    {
        public int ID { get; set; }

        public string Author { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }

        public int PostID { get; set; }
        public string Body { get; set; }

        public bool IsPublished { get; set; }
    }
}