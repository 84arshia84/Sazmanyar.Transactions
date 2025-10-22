using AppCore.Entities.FactorInformation.FactorNettingProcessItems;
using AppCore.Entities.FactorInformation.FactorPayments;
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Entities.SettingEntities.HowToPays;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.FactorConfigs
{
    internal class FactorPaymentConfiguration : IEntityTypeConfiguration<FactorPayment>
    {
        public void Configure(EntityTypeBuilder<FactorPayment> builder)
        {
            builder.ToTable("FactorPayments", "TAM");

            builder.HasKey(fp => fp.Id);

            builder.HasOne<Factor>(fp => fp.Factor)
             .WithMany(f => f.FactorPayments)
             .HasForeignKey(fs => fs.FactorId);

            builder.HasOne<HowToPay>(fp => fp.HowToPay)
            .WithMany(h => h.FactorPayments)
            .HasForeignKey(fs => fs.HowToPayId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Currency>(fp => fp.Currency)
            .WithMany(c => c.FactorPayments)
            .HasForeignKey(fs => fs.CurrencyId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Property(fp => fp.PaymentAmount).HasColumnType("Decimal(30,5)");

            builder.Property(fp => fp.AccelerationRate).HasColumnType("Decimal(30,5)");
        }
    }
}
