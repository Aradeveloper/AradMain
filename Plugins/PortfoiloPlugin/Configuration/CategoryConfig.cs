using PortfoiloPlugin.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PortfoiloPlugin.Configuration
{
    public class CategoryConfig : EntityTypeConfiguration<Category>
    {
        public CategoryConfig()
        {
            HasMany(x => x.Posts).WithRequired(x => x.Category).WillCascadeOnDelete(true);
            Property(p => p.Name)
            .HasMaxLength(300)
            .IsRequired()
            .HasColumnAnnotation("Index",
                new IndexAnnotation(new IndexAttribute("IX_CategoryName") { IsUnique = true }));
        }
    }
}