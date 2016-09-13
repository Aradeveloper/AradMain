using PortfoiloPlugin.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PortfoiloPlugin.Configuration
{
    public class PortfoiloConfig : EntityTypeConfiguration<Portfoilo>
    {
        public PortfoiloConfig()
        {
        }
    }
}