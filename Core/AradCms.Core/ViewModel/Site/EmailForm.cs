using System.ComponentModel.DataAnnotations;

namespace AradCms.Core.ViewModel.Site
{
    public class EmailForm
    {
        [Required(ErrorMessage = "لطفا نام خود را وارد نمایید"), Display(Name = "نام شما"), DataType(DataType.Text)]
        public string FromName { get; set; }

        [Required(ErrorMessage = "لطفا ایمیل خود را وارد نمایید"), Display(Name = "ایمیل شما"), EmailAddress]
        public string FromEmail { get; set; }

        [Display(Name = "موضوع ایمیل"), DataType(DataType.Text)]
        public string SubjectEmail { get; set; }

        [Required(ErrorMessage = "لطفا پیام خود را وارد نمایید"), Display(Name = "پیام شما")]
        public string Message { get; set; }

        [Display(Name = "شماره تماس"), Phone]
        public string PhoneNumber { get; set; }

        public string EmailReciver { get; set; }
    }
}