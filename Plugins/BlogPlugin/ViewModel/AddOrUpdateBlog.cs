using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace BlogPlugin.ViewModel
{
    public class AddOrUpdateBlog
    {
        public int Id { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "منتشر شود ؟")]
        public bool IsActive { get; set; }

        [Display(Name = "پسوند لینک")]
        public string Slug { get; set; }

        [Display(Name = "تصویر")]
        public string BlogImage { get; set; }
    }
}