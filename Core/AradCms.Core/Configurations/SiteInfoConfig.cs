using AradCms.Core.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace AradCms.Core.Configurations
{
    public class SiteInfoConfig : EntityTypeConfiguration<SiteInfo>
    {
        public SiteInfoConfig()
        {
            Property(x => x.SiteName).HasMaxLength(300)
             .IsRequired().HasColumnAnnotation("Index",
                 new IndexAnnotation(new IndexAttribute("IX_SiteName") { IsUnique = true }));
        }
    }
}