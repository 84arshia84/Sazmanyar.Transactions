using AppCore.Entities.PriceListEntities.PriceLists;
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
    internal class CountyConfiguration : IEntityTypeConfiguration<County>
    {
        public void Configure(EntityTypeBuilder<County> builder)
        {
            builder.ToTable("County", "TAM");

            builder.HasKey(x => x.Id);

            builder.HasOne<Province>(co => co.Province)
            .WithMany(p => p.County)
            .HasForeignKey(co => co.ProvinceId);
        }
    }
}
