using BlogPlugin.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace BlogPlugin.Configuration
{
    public class BlogPostConfig : EntityTypeConfiguration<BlogPost>
    {
        public BlogPostConfig()
        {
            HasMany(x => x.PostComments).WithRequired(x => x.Post).WillCascadeOnDelete(true);

            HasMany(x => x.Tag).WithMany(x => x.Posts).Map(x =>
            {
                x.ToTable("PostTags");
                x.MapLeftKey("PostId");
                x.MapRightKey("TagId");
            }

               );
        }
    }
}