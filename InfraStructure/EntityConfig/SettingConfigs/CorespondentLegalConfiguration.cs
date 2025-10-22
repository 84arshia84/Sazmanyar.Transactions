using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class CorespondentLegalConfiguration : IEntityTypeConfiguration<CorespondentLegal>
    {
        public void Configure(EntityTypeBuilder<CorespondentLegal> builder)
        {
            builder.HasOne(c => c.Province)
               .WithMany(p => p.CorespondentLegal)
               .HasForeignKey(c => c.ProvincId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.County)
              .WithMany(co => co.CorespondentLegal)
              .HasForeignKey(c => c.CountyId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.City)
              .WithMany(ci => ci.CorespondentLegal)
              .HasForeignKey(c => c.CityId)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
