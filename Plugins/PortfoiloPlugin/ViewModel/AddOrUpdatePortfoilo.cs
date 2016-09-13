using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace PortfoiloPlugin.ViewModel
{
    public class AddOrUpdatePortfoilo
    {
        public int Id { get; set; }

        [Display(Name = "تیتر")]
        public string Title { get; set; }

        [Display(Name = "خلاصه")]
        [DataType(DataType.Html), AllowHtml]
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
    }
}