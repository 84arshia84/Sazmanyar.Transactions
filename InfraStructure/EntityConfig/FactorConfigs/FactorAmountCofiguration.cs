using AppCore.Entities.FactorInformation.FactorAmounts;
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
    internal class FactorAmountCofiguration : IEntityTypeConfiguration<FactorAmount>
    {
        public void Configure(EntityTypeBuilder<FactorAmount> builder)
        {
            builder.ToTable("FactorAmount", "TAM");

            builder.HasKey(fa => fa.Id);

            builder.HasOne<Currency>(fa => fa.Currency)
             .WithMany(c => c.FactorAmounts)
             .HasForeignKey(fa => fa.CurrencyId);

            builder.HasOne<Factor>(fa => fa.Factor)
             .WithMany(f => f.FactorAmounts)
             .HasForeignKey(fa => fa.FactorId);

            builder.Property(fa => fa.RequestedAmount)
                .HasColumnType("Decimal(30,5)").HasDefaultValue(0);

            builder.Property(fd => fd.NettedAmount)
                .HasColumnType("Decimal(30,5)").HasDefaultValue(0);
        }
    }
}
