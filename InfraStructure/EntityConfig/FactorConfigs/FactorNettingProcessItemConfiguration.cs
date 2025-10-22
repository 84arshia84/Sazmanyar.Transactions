using AppCore.Entities.FactorInformation.FactorNettingProcessItems;
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.SettingEntities.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.FactorConfigs
{
    internal class FactorNettingProcessItemConfiguration : IEntityTypeConfiguration<FactorNettingProcessItem>
    {
        public void Configure(EntityTypeBuilder<FactorNettingProcessItem> builder)
        {
            builder.ToTable("FactorNettingProcessItems", "TAM");

            builder.HasKey(fn => fn.Id);

            builder.HasOne<Factor>(fn => fn.Factor)
             .WithMany(f => f.FactorNettingProcessItems)
             .HasForeignKey(fs => fs.FactorId);

            builder.HasOne<Currency>(fn => fn.Currency)
             .WithMany(c => c.FactorNettingProcessItems)
             .HasForeignKey(fs => fs.CurrencyId);

            builder.Property(fn => fn.Amount).HasColumnType("Decimal(30,5)");

            builder.Property(fn => fn.Percentage).HasColumnType("Decimal(8,5)");

            builder.Property(fn => fn.Title).HasMaxLength(150);
        }
    }
}
