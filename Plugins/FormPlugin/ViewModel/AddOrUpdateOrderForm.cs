using FormPlugin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormPlugin.ViewModel
{
    public class AddOrUpdateOrderForm
    {
        public int Id { get; set; }

        [Display(Name = "نام و نام خانوادگي"), Required(ErrorMessage = "لطفا نام و نام خانوادگی را وارد نمایید")]
        public string Name { get; set; }

        [Display(Name = "شركت /موسسه")]
        public string Company { get; set; }

        [Required(ErrorMessage = "لطفا شماره تماس را وارد نمایید")]
        [Display(Name = "شماره تماس")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"(09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4})", ErrorMessage = "Not a valid Phone number")]
        public string Phone { get; set; }

        [Display(Name = "ایمیل"), DataType(DataType.EmailAddress, ErrorMessage = "لطفا ایمیل را درست وارد نمایید")]
        public string Email { get; set; }

        [Display(Name = "وب سایت"), DataType(DataType.Url)]
        public string Website { get; set; }

        [Display(Name = "نشانی"), DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "موضوع"), DataType(DataType.Text), Required(ErrorMessage = "لطفا موضوع سفارش را وارد نمایید")]
        public string Subject { get; set; }

        [Display(Name = "توضیح"), DataType(DataType.Html), AllowHtml]
        public string Description { get; set; }

        [Display(Name = "کد رهگیری"), DataType(DataType.Text)]
        public string TrackingCode { get; set; }

        [Display(Name = "وضعیت")]
        public FormStatus Status { get; set; }
    }
}