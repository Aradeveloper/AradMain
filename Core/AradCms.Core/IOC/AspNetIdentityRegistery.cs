using AradCms.Core.Context;
using AradCms.Core.IService;
using AradCms.Core.Model;
using AradCms.Core.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using StructureMap;
using StructureMap.Web;
using System;
using System.Data.Entity;
using System.Web;

namespace AradCms.Core.IOC
{
    public class AspNetIdentityRegistery : Registry
    {
        public AspNetIdentityRegistery()
        {
            For<ApplicationDbContext>().HybridHttpOrThreadLocalScoped()
                               .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());
            For<DbContext>().HybridHttpOrThreadLocalScoped()
                 .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());

            For<IUserStore<ApplicationUser, string>>()
                 .HybridHttpOrThreadLocalScoped()
                 .Use<UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>>();

            For<IRoleStore<ApplicationRole, string>>()
                 .HybridHttpOrThreadLocalScoped()
                 .Use<RoleStore<ApplicationRole, string, ApplicationUserRole>>();

            For<IAuthenticationManager>()
                 .Use(() => HttpContext.Current.GetOwinContext().Authentication);

            For<IApplicationSignInManager>()
                 .HybridHttpOrThreadLocalScoped()
                 .Use<ApplicationSignInManager>();

            For<IApplicationRoleManager>()
                 .HybridHttpOrThreadLocalScoped()
                 .Use<ApplicationRoleManager>();

            For<IIdentityMessageService>().Use<SmsService>();
            For<IIdentityMessageService>().Use<AradCms.Core.Service.EmailService>();

            For<IApplicationUserManager>().HybridHttpOrThreadLocalScoped()
               .Use<ApplicationUserManager>()
               .Ctor<IIdentityMessageService>("smsService").Is<SmsService>()
               .Ctor<IIdentityMessageService>("emailService").Is<EmailService>()
               .Setter<IIdentityMessageService>(userManager => userManager.SmsService).Is<SmsService>()
               .Setter<IIdentityMessageService>(userManager => userManager.EmailService).Is<EmailService>();

            For<ApplicationUserManager>().HybridHttpOrThreadLocalScoped()
                 .Use(context => (ApplicationUserManager)context.GetInstance<IApplicationUserManager>());

            For<ICustomRoleStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomRoleStore>();

            For<ICustomUserStore>()
                  .HybridHttpOrThreadLocalScoped()
                  .Use<CustomUserStore>();
        }
    }
}