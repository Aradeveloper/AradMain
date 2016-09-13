﻿using BlogPlugin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPlugin.ViewModel
{
    public class PostDetail
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
        public bool Published { get; set; }

        public string Summary { get; set; }

        [DataType(DataType.Html), AllowHtml]
        public string Body { get; set; }

        public string Slug { get; set; }

        public string BlogName { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int VisitCount { get; set; }

        [Display(Name = "ایجاد کننده")]
        public string UserName { get; set; }

        [Display(Name = "لوگو"), DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }

        public IList<Tag> Tags { get; set; }

        [Display(Name = "وضعیت کامنت")]
        public bool CommentStatuse { get; set; }
    }
}