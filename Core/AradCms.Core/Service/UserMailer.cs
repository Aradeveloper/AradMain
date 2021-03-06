﻿using AradCms.Core.IService;
using AradCms.Core.ViewModel.Account;
using Mvc.Mailer;
using System.Text;

namespace AradCms.Core.Service
{
    public class UserMailer : MailerBase, IUserMailer
    {
        public UserMailer()
        {
            MasterName = "_Layout";
        }

        public MvcMailMessage ResetPassword(EmailViewModel resetPasswordEmail)
        {
            ViewData.Model = resetPasswordEmail;
            return Populate(x =>
            {
                x.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                x.BodyEncoding = Encoding.UTF8;
                x.Subject = resetPasswordEmail.Subject;
                x.ViewName = resetPasswordEmail.ViewName;
                x.Body = resetPasswordEmail.Message;
                x.To.Add(resetPasswordEmail.To);
            });
        }

        public MvcMailMessage ConfirmAccount(EmailViewModel confirmAccountEmail)
        {
            ViewData.Model = confirmAccountEmail;
            return Populate(x =>
            {
                x.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                x.BodyEncoding = Encoding.UTF8;
                x.Body = confirmAccountEmail.Message;
                x.Subject = confirmAccountEmail.Subject;
                x.ViewName = confirmAccountEmail.ViewName;
                x.To.Add(confirmAccountEmail.To);
            });
        }
    }
}