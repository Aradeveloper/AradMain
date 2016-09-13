using BlogPlugin.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace BlogPlugin.Configuration
{
    public class PostCommentConfig : EntityTypeConfiguration<PostComment>
    {
        public PostCommentConfig()
        {
            HasMany(a => a.Childeren).WithOptional(a => a.Parent).HasForeignKey(a => a.ParentId);
            // HasOptional(a => a.Post).WithMany(a => a.PostComments).WillCascadeOnDelete();
            Property(a => a.ParentId).IsOptional();
        }
    }
}