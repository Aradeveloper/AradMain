using FormPlugin.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace FormPlugin.Configoration
{
    public class OrderFormConfig : EntityTypeConfiguration<OrderForm>
    {
        public OrderFormConfig()
        {
            Property(x => x.TrackingCode).HasMaxLength(100)
             .IsRequired().HasColumnAnnotation("Index",
                 new IndexAnnotation(new IndexAttribute("IX_TrackingCode") { IsUnique = true }));
        }
    }
}