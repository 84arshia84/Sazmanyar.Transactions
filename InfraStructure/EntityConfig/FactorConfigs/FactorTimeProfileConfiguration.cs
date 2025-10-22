using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.FactorInformation.FactorTimeProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.FactorConfigs
{
    internal class FactorTimeProfileConfiguration : IEntityTypeConfiguration<FactorTimeProfile>
    {
        public void Configure(EntityTypeBuilder<FactorTimeProfile> builder)
        {
            builder.ToTable("FactorTimeProfiles", "TAM");

            builder.HasKey(x => x.Id);

            builder.Property(ft => ft.FactorPeriod).HasMaxLength(100);

        }
    }
}
