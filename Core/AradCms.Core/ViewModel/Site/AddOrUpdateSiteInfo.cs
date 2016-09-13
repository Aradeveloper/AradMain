using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AradCms.Core.ViewModel.Site
{
    public class AddOrUpdateSiteInfo
    {
        public int Id { get; set; }

        [Display(Name = "نام سایت"), DataType(DataType.Text), Required(ErrorMessage = "لطفا نام سایت را وارد نمایید")]
        public string SiteName { get; set; }

        [Display(Name = "درباره سایت"), DataType(DataType.Html), AllowHtml]
        public string SiteAboute { get; set; }

        [Display(Name = "متن تماس با سایت"), DataType(DataType.Html), AllowHtml]
        public string SiteContact { get; set; }

        [Display(Name = "آدرس وبسایت")]
        public string SiteAddress { get; set; }

        [Display(Name = "شماره تماس وبسایت")]
        public string SitePhone { get; set; }

        [AllowHtml]
        public string GooglemapCode { get; set; }

        public string EmailUsername { get; set; }
        public string EmailPassword { get; set; }
        public string Smtpserver { get; set; }
        public string AboutImage { get; set; }
    }
}