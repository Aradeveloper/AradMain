using FormPlugin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FormPlugin.ViewModel
{
    public class OrderFormDetail
    {
        public int Id { get; set; }

        [Display(Name = "نام و نام خانوادگي")]
        public string Name { get; set; }

        [Display(Name = "شركت /موسسه")]
        public string Company { get; set; }

        [Display(Name = "شماره تماس"), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "ایمیل"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "وب سایت"), DataType(DataType.Url)]
        public string Website { get; set; }

        [Display(Name = "نشانی"), DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "موضوع"), DataType(DataType.Text)]
        public string Subject { get; set; }

        [Display(Name = "توضیح"), DataType(DataType.Html)]
        public string Description { get; set; }

        [Display(Name = "کد رهگیری"), DataType(DataType.Text)]
        public string TrackingCode { get; set; }

        [Display(Name = "وضعیت")]
        public FormStatus Status { get; set; }
    }
}