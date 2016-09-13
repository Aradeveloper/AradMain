using AradCms.Core.IOC;
using AradCms.Core.IService;
using AradCms.Core.Model;
using AradCms.Core.Plugin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using StructureMap.Web;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(AradMain.Startup))]

namespace AradMain
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        private static void ConfigureAuth(IAppBuilder appBuilder)
        {
            //appBuilder.UseStaticFiles();
            ProjectObjectFactory.Container.Configure(config => config.For<IDataProtectionProvider>()
                .HybridHttpOrThreadLocalScoped()
                .Use(() => appBuilder.GetDataProtectionProvider()));
            var plugins = ProjectObjectFactory.Container.GetAllInstances<IPlugin>().ToList();

            appBuilder.CreatePerOwinContext(() => ProjectObjectFactory.Container.GetInstance<IApplicationUserManager>());

            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieName = "my-very-own-cookie-name",
                LoginPath = new PathString("/Administration/Account/Login"),
                ExpireTimeSpan = TimeSpan.FromDays(30),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity =
                            ProjectObjectFactory.Container.GetInstance<IApplicationUserManager>().OnValidateIdentity()
                }
            });
            var permissionsListToAdd = new List<ApplicationPermission>();
            foreach (var plugin in plugins)
            {
                permissionsListToAdd.AddRange(plugin.ConfigPermissions());
            }
            ProjectObjectFactory.Container.GetInstance<IApplicationRoleManager>()
             .SeedDatabase(permissionsListToAdd);
            ProjectObjectFactory.Container.GetInstance<IApplicationUserManager>()
                   .SeedDatabase();
            appBuilder.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}