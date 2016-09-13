using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace SliderPlugin.Models
{
    public class SliderModel
    {
        [Key]
        public int Id { get; set; }

        public string TitleOne { get; set; }
        public string TitleTwo { get; set; }
        public string TitleThree { get; set; }

        [DataType(DataType.Html), AllowHtml]
        public string Body { get; set; }

        public string ImagePath { get; set; }
        public bool Published { get; set; }
    }
}