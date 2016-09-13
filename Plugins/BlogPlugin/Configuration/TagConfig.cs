using BlogPlugin.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace BlogPlugin.Configuration
{
    public class TagConfig : EntityTypeConfiguration<Tag>
    {
        public TagConfig()
        {
            Property(p => p.Name)
             .HasMaxLength(200)
            .IsRequired()
            .HasColumnAnnotation("Index",
                new IndexAnnotation(new IndexAttribute("IX_TagName") { IsUnique = true }));
        }
    }
}