using AradCms.Core.Context;
using AradCms.Core.Helpers;
using AradCms.Core.IOC;

//using ElmahEFLogger.CustomElmahLogger;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace AradMain
{
    public static class CustomAppConfig
    {
        public static void Config()
        {
            // disable response header for protection  attak
            MvcHandler.DisableMvcResponseHeader = true;

            // change captcha provider for using cookie

            //Set current Controller factory as StructureMapControllerFactory
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            //set current Filter factory as StructureMapFitlerProvider
            var filterProider = FilterProviders.Providers.Single(p => p is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(filterProider);
            FilterProviders.Providers.Add(ProjectObjectFactory.Container.GetInstance<StructureMapFilterProvider>());

            Database.SetInitializer<ApplicationDbContext>(null);
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            //ProjectObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();

            var defaultJsonFactory = ValueProviderFactories.Factories
                .OfType<JsonValueProviderFactory>().FirstOrDefault();
            var index = ValueProviderFactories.Factories.IndexOf(defaultJsonFactory);
            ValueProviderFactories.Factories.Remove(defaultJsonFactory);
            ValueProviderFactories.Factories.Insert(index, new JsonNetValueProviderFactory());

            //ad interception for logg EF errors
            //DbInterception.Add(new ElmahEfInterceptor());
        }
    }
}