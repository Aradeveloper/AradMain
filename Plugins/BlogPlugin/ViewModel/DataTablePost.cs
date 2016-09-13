using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogPlugin.ViewModel
{
    public class DataTablePost
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public DateTime Timestamp { get; set; }

        [DataType(DataType.Html)]
        public string Summary { get; set; }

        [DataType(DataType.Html)]
        public string Body { get; set; }

        public string Slug { get; set; }
        public int BlogId { get; set; }
        public int CommentCount { get; set; }
        public bool Published { get; set; }
        public string BlogName { get; set; }
        public string UserName { get; set; }
        public string PostImage { get; set; }
        public int VisitCount { get; set; }
        public int CommentProve { get; set; }

        [Display(Name = "کامنت تایید نشده")]
        public int CommentUnprove { get; set; }


        [Display(Name = "وضعیت کامنت")]
        public bool CommentStatuse { get; set; }
    }
}