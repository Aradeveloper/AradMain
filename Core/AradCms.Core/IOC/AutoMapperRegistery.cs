using AradCms.Core.AutoMapperProfiles;
using AutoMapper;

using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Web;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AradCms.Core.IOC
{
    public class AutoMapperRegistery : Registry
    {
        public AutoMapperRegistery()
        {
            var profiles =
                 typeof(AutoMapperRegistery).Assembly.GetTypes()
                     .Where(t => typeof(Profile).IsAssignableFrom(t))
                     .Select(t => (Profile)Activator.CreateInstance(t));

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            For<MapperConfiguration>().Use(config);
            For<IMapper>().Use(ctx => ctx.GetInstance<MapperConfiguration>().CreateMapper(ctx.GetInstance));
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<UserProfile>();
                scanner.AddAllTypesOf<Profile>().NameBy(item => item.Name);

                scanner.ConnectImplementationsToTypesClosing(typeof(ITypeConverter<,>))
                       .OnAddedPluginTypes(t => t.HybridHttpOrThreadLocalScoped());

                scanner.ConnectImplementationsToTypesClosing(typeof(IValueResolver<,,>))
                    .OnAddedPluginTypes(t => t.HybridHttpOrThreadLocalScoped());
            });
        }
    }
}