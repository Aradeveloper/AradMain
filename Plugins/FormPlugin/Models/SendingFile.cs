using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FormPlugin.Models
{
    public class SendingFile
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Company { get; set; }

        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Subject { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public DateTime SendTime { get; set; }
        public string TrackingCode { get; set; }
        public FormStatus Status { get; set; }

        public SendingFile()
        {
            SendTime = DateTime.Now;
        }
    }
}