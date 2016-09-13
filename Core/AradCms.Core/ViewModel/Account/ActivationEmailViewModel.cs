using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AradCms.Core.ViewModel.Account
{
    public class ActivationEmailViewModel
    {
        [Required(ErrorMessage = "وارد کردن ایمیل ضروریست")]
        [EmailAddress(ErrorMessage = "ایمیل را به شکل صحیح وارد کنید")]
        [DisplayName("ایمیل")]
        [StringLength(450, ErrorMessage = "حداکثر طول ایمیل 450 حرف است")]
        [Remote("IsEmailAvailable", "Account", "", ErrorMessage = "ایمیل مورد نظر یافت نشد", HttpMethod = "POST")]
        public string Email { get; set; }
    }
}