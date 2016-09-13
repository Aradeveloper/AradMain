﻿using System.ComponentModel.DataAnnotations;

namespace AradCms.Core.ViewModel.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "وارد کردن ایمیل ضروریست")]
        [EmailAddress(ErrorMessage = "ایمیل را به شکل صحیح وارد کنید")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }
}