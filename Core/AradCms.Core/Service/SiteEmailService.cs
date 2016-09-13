using AradCms.Core.IService;
using AradCms.Core.ViewModel;
using AradCms.Core.ViewModel.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AradCms.Core.Service
{
    public class SiteEmailService : ISiteEmailService
    {
        private readonly ISiteInfoService _siteinfo;

        public SiteEmailService(ISiteInfoService siteinfo)
        {
            _siteinfo = siteinfo;
        }

        public async Task<bool> SendEmail(EmailForm model)
        {
            try
            {
                var infomodel = _siteinfo.Find(1);
                if (model.EmailReciver == null)
                {
                    model.EmailReciver = infomodel.EmailUsername;
                }
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(model.EmailReciver));  // replace with valid value
                message.From = new MailAddress(infomodel.EmailUsername);  // replace with valid value
                message.Subject = model.SubjectEmail;
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = infomodel.EmailUsername,  // replace with valid value
                        Password = infomodel.EmailPassword  // replace with valid value
                    };

                    smtp.Credentials = credential;
                    smtp.Host = infomodel.Smtpserver;
                    smtp.Port = infomodel.smtpport;
                    smtp.EnableSsl = false;
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}