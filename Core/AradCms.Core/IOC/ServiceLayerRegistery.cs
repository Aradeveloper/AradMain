using AradCms.Core.Service;

using StructureMap;

namespace AradCms.Core.IOC
{
    public class ServiceLayerRegistery : Registry
    {
        public ServiceLayerRegistery()
        {
            Scan(scanner =>
            {
                scanner.WithDefaultConventions();
                scanner.AssemblyContainingType<UserMailer>();
            });
        }
    }
}