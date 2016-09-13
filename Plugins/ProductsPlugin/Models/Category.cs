using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsPlugin.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public string Slug { get; set; }
        public string ImageName { get; set; }
        public IList<Product> Products { get; set; }
    }
}