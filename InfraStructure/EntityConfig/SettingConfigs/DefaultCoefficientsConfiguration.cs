using AppCore.Entities.SettingEntities.DefaultCoefficients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class DefaultCoefficientsConfiguration : IEntityTypeConfiguration<DefaultCoefficients>
    {
        public void Configure(EntityTypeBuilder<DefaultCoefficients> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("DefaultCoefficients", "TAM");

            builder.Property(x => x.DefaultRows)
                .HasMaxLength(200);

            builder.Property(x => x.Title)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.DefaultCoefficient)
                .HasColumnType("Decimal(30,5)");
        }
    }
}
