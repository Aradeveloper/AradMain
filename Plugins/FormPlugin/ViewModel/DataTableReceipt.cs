using FormPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormPlugin.ViewModel
{
    public class DataTableReceipt
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public ReciptType ReciptionType { get; set; }
        public DateTime ReciptTime { get; set; }
        public string ReciptCode { get; set; }
        public string BankName { get; set; }
    }
}