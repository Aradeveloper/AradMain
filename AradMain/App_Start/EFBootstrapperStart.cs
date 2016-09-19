using AradCms.Core.Context;
using AradCms.Core.IOC;
using AradCms.Core.Migrations;
using AradCms.Core.Plugin;
using AradMain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(EFBootstrapperStart), "Start")]

namespace AradMain
{
    public static class EFBootstrapperStart
    {
        public static void Start()
        {
            var plugins = ProjectObjectFactory.Container.GetAllInstances<IPlugin>().ToList();
            using (var uow = ProjectObjectFactory.Container.GetInstance<IUnitOfWork>())
            {
                initDatabase(uow, plugins);
                runDatabaseSeeders(uow, plugins);
            }
        }

        private static void initDatabase(IUnitOfWork uow, IEnumerable<IPlugin> plugins)
        {
            foreach (var plugin in plugins)
            {
                var efBootstrapper = plugin.GetEfBootstrapper();
                if (efBootstrapper == null) continue;

                uow.SetDynamicEntities(efBootstrapper.DomainEntities);
                uow.SetConfigurationsAssemblies(efBootstrapper.ConfigurationsAssemblies);
            }

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            uow.ForceDatabaseInitialize();
        }

        private static void runDatabaseSeeders(IUnitOfWork uow, IEnumerable<IPlugin> plugins)
        {
            foreach (var plugin in plugins)
            {
                var efBootstrapper = plugin.GetEfBootstrapper();
                if (efBootstrapper == null || efBootstrapper.DatabaseSeeder == null) continue;

                efBootstrapper.DatabaseSeeder(uow);
                uow.SaveAllChanges();
            }
        }
    }
}