using BlogPlugin.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace BlogPlugin.Configuration
{
    public class BlogConfig : EntityTypeConfiguration<Blog>
    {
        public BlogConfig()
        {
            HasMany(x => x.Posts).WithRequired(x => x.Blog).WillCascadeOnDelete(true);
            Property(p => p.Name)
            .HasMaxLength(300)
            .IsRequired()
            .HasColumnAnnotation("Index",
                new IndexAnnotation(new IndexAttribute("IX_BlogName") { IsUnique = true }));
        }
    }
}