using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace BlogPlugin.ViewModel
{
    public class AddOrUpdatePost
    {
        public int Id { get; set; }

        [Display(Name = "تیتر")]
        public string Title { get; set; }

        [Display(Name = "خلاصه")]
        public string Summary { get; set; }

        [Display(Name = "متن")]
        [DataType(DataType.Html), AllowHtml]
        public string Body { get; set; }

        [Display(Name = "پسوند لینک")]
        public string Slug { get; set; }

        public int BlogId { get; set; }

        [Display(Name = "آیا منتشر شود ؟")]
        public bool Published { get; set; }

        public string UserName { get; set; }

        [Display(Name = "تصویر متن")]
        public string PostImage { get; set; }

        [Display(Name = "تگ")]
        //[Remote("TagNameExist", "AdminPost", "Blog", ErrorMessage = "این تگ قبلا در سیستم ثبت شده است", HttpMethod = "POST")]
        public string Tags { get; set; }

        [Display(Name = "وضعیت کامنت")]
        public bool CommentStatuse { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
    }
}