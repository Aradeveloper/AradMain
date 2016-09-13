using AradCms.Core.ViewModel.Account;
using Mvc.Mailer;

namespace AradCms.Core.IService
{
    public interface IUserMailer
    {
        MvcMailMessage ResetPassword(EmailViewModel resetPasswordEmail);

        MvcMailMessage ConfirmAccount(EmailViewModel confirmAccountEmail);
    }
}