using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogPlugin.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Slug { get; set; }
        public IList<BlogPost> Posts { get; set; }
        public string ImagePath { get; set; }
    }
}