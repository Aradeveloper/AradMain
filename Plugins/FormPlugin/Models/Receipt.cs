using System;
using System.ComponentModel.DataAnnotations;

namespace FormPlugin.Models
{
    public class Receipt
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public ReciptType ReciptionType { get; set; }
        public DateTime ReciptTime { get; set; }
        public string ReciptCode { get; set; }
        public string BankName { get; set; }
        public string TrackingCode { get; set; }
        public string ReciptImage { get; set; }
    }
}