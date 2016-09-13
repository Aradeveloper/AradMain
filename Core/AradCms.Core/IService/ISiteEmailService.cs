using AradCms.Core.ViewModel.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AradCms.Core.IService
{
    public interface ISiteEmailService
    {
        Task<bool> SendEmail(EmailForm model);
    }
}