using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FormPlugin.Models
{
    public class OrderForm
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Company { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        public string Website { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        public string Subject { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SendTime { get; set; }

        public string TrackingCode { get; set; }
        public FormStatus Status { get; set; }
    }
}