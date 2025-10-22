using AppCore.Entities.SettingEntities.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City", "TAM");

            builder.HasKey(x => x.Id);

            builder.HasOne<County>(ci => ci.County)
            .WithMany(p => p.Cities)
            .HasForeignKey(ci => ci.CountyId);

            builder.HasOne<Province>(ci => ci.Province)
            .WithMany(p => p.Cities)
            .HasForeignKey(ci => ci.ProvinceId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
