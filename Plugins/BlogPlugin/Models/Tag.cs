using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogPlugin.Models
{
    public class Tag
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public IList<BlogPost> Posts { get; set; }
    }
}