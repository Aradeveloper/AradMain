using SliderPlugin.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace SliderPlugin.Configuration
{
    public class SliderModelConfig : EntityTypeConfiguration<SliderModel>
    {
        public SliderModelConfig()
        {
            Property(a => a.TitleOne).HasMaxLength(200);
        }
    }
}