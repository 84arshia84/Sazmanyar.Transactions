using AppCore.Entities.SettingEntities.LookUpTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class LookUpTableConfiguration : IEntityTypeConfiguration<LookUpTable>
    {
        public void Configure(EntityTypeBuilder<LookUpTable> builder)
        {
            builder.ToTable("LookUpTables", "TAM");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.Title).HasMaxLength(200);

            builder.HasMany(x => x.LookUpTableInsides);

            builder.HasMany(x => x.CheckLists);
        }
    }
}
