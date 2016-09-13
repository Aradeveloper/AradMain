using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SliderPlugin.ViewModel
{
    public class AddOrUpdate
    {
        public int Id { get; set; }

        [Display(Name = "تیتر اول")]
        public string TitleOne { get; set; }

        [Display(Name = "تیتر دوم")]
        public string TitleTwo { get; set; }

        [Display(Name = "تیتر سوم")]
        public string TitleThree { get; set; }

        [Display(Name = "متن پیوستی")]
        [DataType(DataType.Html), AllowHtml]
        public string Body { get; set; }

        [Display(Name = "تصویر اسلاید")]
        public string ImagePath { get; set; }

        [Display(Name = "آیا منتشر شود؟")]
        public bool Published { get; set; }
    }
}