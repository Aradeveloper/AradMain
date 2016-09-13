using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductsPlugin.Models
{
    public class ProductImages
    {
        [Key]
        public int Id { get; set; }

        public string ImageName { get; set; }
        public Product Product { get; set; }
    }
}