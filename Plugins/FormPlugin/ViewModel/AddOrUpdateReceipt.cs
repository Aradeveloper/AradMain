using FormPlugin.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace FormPlugin.ViewModel
{
    public class AddOrUpdateReceipt
    {
        public int Id { get; set; }

        [Display(Name = "نام/نام خانوادگی")]
        public string Name { get; set; }

        [Display(Name = "نوع واریز")]
        public ReciptType ReciptionType { get; set; }

        [Display(Name = "شناسه واریز"), Required(ErrorMessage = "لطفا شناسه واریز را وارد نمایید")]
        public string ReciptCode { get; set; }

        [Display(Name = "نام بانک"), Required(ErrorMessage = "لطفا نام بانک را وارد نمایید")]
        public string BankName { get; set; }

        [Display(Name = "کد رهگیری سفارش"), Required(ErrorMessage = "لطفا کد رهگیری سفارش خود را وارد نمایید")]
        public string TrackingCode { get; set; }

        [Display(Name = "قبض واریز"), Required(ErrorMessage = "لطفا عکس قبض واریز را وارد نمایید")]
        public string ReciptImage { get; set; }

        [Display(Name = "تاریخ واریز"), Required(ErrorMessage = "لطفا تاریخ واریز را انتخاب نمایید")]
        [UIHint("PersianDatePicker")]
        public DateTime ReciptTime { get; set; }
    }
}