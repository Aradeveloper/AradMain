﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PortfoiloPlugin.ViewModel
{
    public class DataTablePortfoilo
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public DateTime Timestamp { get; set; }

        [DataType(DataType.Html)]
        public string Summary { get; set; }

        [DataType(DataType.Html)]
        public string Body { get; set; }

        public string Slug { get; set; }
        public int CategoryId { get; set; }

        public bool Published { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public string PostImage { get; set; }
    }
}