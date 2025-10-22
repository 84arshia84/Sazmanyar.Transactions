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
    internal class CorespondentRealConfiguration : IEntityTypeConfiguration<CorespondentReal>
    {
        public void Configure(EntityTypeBuilder<CorespondentReal> builder)
        {
            builder.HasOne(c => c.Province)
               .WithMany(p => p.CorespondentReals)
               .HasForeignKey(c => c.ProvincId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.County)
              .WithMany(co => co.CorespondentReals)
              .HasForeignKey(c => c.CountyId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.City)
              .WithMany(ci => ci.CorespondentReals)
              .HasForeignKey(c => c.CityId)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
