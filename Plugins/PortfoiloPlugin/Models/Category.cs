using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PortfoiloPlugin.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Slug { get; set; }
        public IList<Portfoilo> Posts { get; set; }
        public string ImagePath { get; set; }
    }
}