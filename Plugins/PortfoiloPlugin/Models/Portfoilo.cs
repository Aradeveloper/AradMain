using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace PortfoiloPlugin.Models
{
    public class Portfoilo
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Published { get; set; }

        [DataType(DataType.Html), AllowHtml]
        public string Summary { get; set; }

        [DataType(DataType.Html), AllowHtml]
        public string Body { get; set; }

        public string Slug { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int VisitCount { get; set; }

        [Display(Name = "ایجاد کننده")]
        public string UserName { get; set; }

        [Display(Name = "لوگو"), DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }
    }
}