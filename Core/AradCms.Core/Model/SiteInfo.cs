using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace AradCms.Core.Model
{
    public class SiteInfo
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        public string SiteName { get; set; }

        [DataType(DataType.Html), AllowHtml]
        public string SiteAboute { get; set; }

        [DataType(DataType.Html)AllowHtml]
        public string SiteContact { get; set; }

        public string SiteAddress { get; set; }

        public string SitePhone { get; set; }
        public string AboutImage { get; set; }

        [AllowHtml]
        public string GooglemapCode { get; set; }

        public string EmailUsername { get; set; }
        public string EmailPassword { get; set; }
        public string Smtpserver { get; set; }
        public int smtpport { get; set; }
    }
}