﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortfoiloPlugin.ViewModel
{
    public class DataTableCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Slug { get; set; }
        public string ImagePath { get; set; }
        public int PostCount { get; set; }
    }
}