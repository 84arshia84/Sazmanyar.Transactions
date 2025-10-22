using AppCore.Entities.SettingEntities.FactorTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class FactorTypeConfiguration : IEntityTypeConfiguration<FactorType>
    {
        public void Configure(EntityTypeBuilder<FactorType> builder)
        {
            builder.ToTable("FactorTypes", "TAM");

            builder.HasKey(ft => ft.ID);
        }
    }
}
