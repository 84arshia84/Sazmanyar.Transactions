using AppCore.Entities.SettingEntities.ContractTypes;
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
    internal class LookUpTableInsideConfiguration : IEntityTypeConfiguration<LookUpTableInside>
    {
        public void Configure(EntityTypeBuilder<LookUpTableInside> builder)
        {
            builder.ToTable("LookUpTableInsides", "TAM");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.Title).HasMaxLength(200);

            builder.HasOne<LookUpTable>(li => li.LookUpTable)
           .WithMany(l => l.LookUpTableInsides)
           .HasForeignKey(li => li.LookUpTableId);
        }
    }
}
