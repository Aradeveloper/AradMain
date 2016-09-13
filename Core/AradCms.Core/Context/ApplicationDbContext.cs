using AradCms.Core.Configurations;
using AradCms.Core.Filters;
using AradCms.Core.Model;
using AradCms.Core.Utility;
using EFSecondLevelCache;
using EntityFramework.BulkInsert.Extensions;
using EntityFramework.Filters;
using Microsoft.AspNet.Identity.EntityFramework;
using RefactorThis.GraphDiff;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AradCms.Core.Context
{
    public class ApplicationDbContext : IdentityDbContext
        <ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>,
        IUnitOfWork
    {
        #region Constructor

        private readonly IList<Assembly> _configurationsAssemblies = new List<Assembly>();
        private readonly IList<Type[]> _dynamicTypes = new List<Type[]>();

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            //this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            //for bulk insert
            //this.Configuration.AutoDetectChangesEnabled = true;
        }

        #endregion Constructor

        #region IUnitOfWork

        public T Update<T>(T entity, System.Linq.Expressions.Expression<Func<IUpdateConfiguration<T>, object>> mapping)
            where T : class, new()
        {
            return this.UpdateGraph(entity, mapping);
        }

        private string[] GetChangedEntityNames()
        {
            return ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added ||
                            x.State == EntityState.Modified ||
                            x.State == EntityState.Deleted)
                .Select(x => ObjectContext.GetObjectType(x.Entity.GetType()).FullName)
                .Distinct()
                .ToArray();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void MarkAsDetached<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Detached;
        }

        public void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public override int SaveChanges()
        {
            return SaveAllChanges();
        }

        public int SaveAllChanges(bool invalidateCacheDependencies = true)
        {
            try
            {
                var result = base.SaveChanges();
                if (!invalidateCacheDependencies) return result;
                var changedEntityNames = GetChangedEntityNames();
                new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            return SaveAllChangesAsync();
        }

        public Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true)
        {
            try
            {
                var result = base.SaveChangesAsync();
                if (!invalidateCacheDependencies) return result;

                var changedEntityNames = GetChangedEntityNames();
                new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
                return result;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
                return null;
            }
        }

        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return ((DbSet<TEntity>)Set<TEntity>()).AddRange(entities);
        }

        public void ForceDatabaseInitialize()
        {
            base.Database.Initialize(force: true);
        }

        public void EnableFiltering(string name)
        {
            this.EnableFilter(name);
        }

        public void EnableFiltering(string name, string parameter, object value)
        {
            this.EnableFilter(name).SetParameter(parameter, value);
        }

        public void DisableFiltering(string name)
        {
            this.DisableFilter(name);
        }

        public void BulkInsertData<T>(IList<T> data)
        {
            this.BulkInsert(data);
        }

        public bool ValidateOnSaveEnabled
        {
            get { return Configuration.ValidateOnSaveEnabled; }
            set { Configuration.ValidateOnSaveEnabled = value; }
        }

        public bool ProxyCreationEnabled
        {
            get { return Configuration.ProxyCreationEnabled; }
            set { Configuration.ProxyCreationEnabled = value; }
        }

        bool IUnitOfWork.AutoDetectChangesEnabled
        {
            get { return Configuration.AutoDetectChangesEnabled; }
            set { Configuration.AutoDetectChangesEnabled = value; }
        }

        public bool ForceNoTracking { get; set; }

        #endregion IUnitOfWork

        #region Override OnModelCreating

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            addConfigurationsFromAssemblies(modelBuilder);
            addPluginsEntitiesDynamically(modelBuilder);
            DbInterception.Add(new FilterInterceptor());
            DbInterception.Add(new YeKeInterceptor());
            base.OnModelCreating(modelBuilder);
            Config(modelBuilder);
            // modelBuilder.Configurations.AddFromAssembly(typeof(SettingConfig).GetTypeInfo().Assembly);
            modelBuilder.Configurations.AddFromAssembly(typeof(SiteInfoConfig).GetTypeInfo().Assembly);
            LoadEntities(typeof(ApplicationUser).GetTypeInfo().Assembly, modelBuilder, "AradCms.Core.Model");
        }

        #endregion Override OnModelCreating

        #region AutoRegisterEntityType

        public void LoadEntities(Assembly asm, DbModelBuilder modelBuilder, string nameSpace)
        {
            var entityTypes = asm.GetTypes()
                .Where(type => type.BaseType != null &&
                               type.Namespace == nameSpace &&
                               type.BaseType == null)
                .ToList();

            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");
            entityTypes.ForEach(type => entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { }));
        }

        #endregion AutoRegisterEntityType

        #region AspNetIdentityConfig

        private static void Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .Filter(UserFilters.DeletedList, a => a.Condition(u => u.IsDeleted))
                .Filter(UserFilters.BannedList, a => a.Condition(u => u.IsBanned))
                .Filter(UserFilters.SystemAccountList, a => a.Condition(u => u.IsSystemAccount))
                .Filter(UserFilters.CanChangeProfilePicList, a => a.Condition(u => u.CanChangeProfilePicture))
                .Filter(UserFilters.CanModifyFirsAndLastNameList, a => a.Condition(u => u.CanModifyFirsAndLastName))
                .Filter(UserFilters.EmailConfirmedList, a => a.Condition(u => u.EmailConfirmed))
                .Filter(UserFilters.CanUploadfileList, a => a.Condition(u => u.CanUploadFile))
                .Filter(UserFilters.ActiveList, a => a.Condition(u => !u.IsBanned))
                .Filter(UserFilters.NotSystemAccountList, a => a.Condition(u => !u.IsSystemAccount))
                .ToTable("Users");

            modelBuilder.Entity<ApplicationRole>()
                .Filter(RoleFilters.ActiveList, a => a.Condition(u => u.IsActive))
                .ToTable("Roles");

            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogins");
        }

        #endregion AspNetIdentityConfig

        #region IDbSets

        public DbSet<ApplicationPermission> ApplicationPermissions { get; set; }
        public DbSet<SiteInfo> SiteInfo { get; set; }

        #endregion IDbSets

        #region StoredProcedures

        [DbFunction("AradCms.Core.Context", "GetUserPermissions")]
        public IList<string> GetUserPermissions(int[] roleIds)
        {
            //            var query = new StringBuilder();
            //            query.Append(
            //                @"
            //    select i.Name as ItemName, f.Name as FirmName, c.Name as CategoryName
            //    from Item i
            //      inner join Firm f on i.FirmId = f.FirmId
            //      inner join Category c on i.CategoryId = c.CategoryId
            //    where c.CategoryId in (");

            //            if (roleIds != null && roleIds.Length > 0)
            //            {
            //                for (var i = 0; i < roleIds.Length; i++)
            //                {
            //                    if (i != 0)
            //                        query.Append(",");
            //                    query.Append(roleIds[i]);
            //                }
            //            }
            //            else
            //            {
            //                query.Append("-1"); // It is for empty result when no one category selected
            //            }
            //            query.Append(")");

            //            var sqlQuery = query.ToString();
            return null;
        }

        #endregion StoredProcedures

        public void SetConfigurationsAssemblies(Assembly[] assemblies)
        {
            if (assemblies == null) return;

            foreach (var assembly in assemblies)
            {
                if (_configurationsAssemblies.Contains(assembly)) continue;
                _configurationsAssemblies.Add(assembly);
            }
        }

        public void SetDynamicEntities(Type[] dynamicTypes)
        {
            if (dynamicTypes == null) return;
            _dynamicTypes.Add(dynamicTypes);
        }

        private void addConfigurationsFromAssemblies(DbModelBuilder modelBuilder)
        {
            foreach (var assembly in _configurationsAssemblies)
            {
                modelBuilder.Configurations.AddFromAssembly(assembly);
            }
        }

        private void addPluginsEntitiesDynamically(DbModelBuilder modelBuilder)
        {
            foreach (var types in _dynamicTypes)
            {
                foreach (var type in types)
                {
                    modelBuilder.RegisterEntityType(type);
                }
            }
        }
    }
}