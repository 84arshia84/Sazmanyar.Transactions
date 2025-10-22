using AppCore.Entities.InvoiceInformations.InvoiceAmounts;
using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.SettingEntities.Currencies;
using InfraStructure.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.InvoiceConfig
{
    internal class InvoiceAmountConfigiuration : IEntityTypeConfiguration<InvoiceAmount>
    {
        public void Configure(EntityTypeBuilder<InvoiceAmount> builder)
        {
            builder.ToTable("InvoiceAmounts", "TAM");

            builder.HasKey(x => x.Id);

            builder.HasOne<InvoiceBaseInformation>(ia => ia.InvoiceBaseInformation)
            .WithMany(i => i.InvoiceAmount)
            .HasForeignKey(ia=>ia.InvocieBaseInformationId);

            builder.HasOne<Currency>(ia => ia.Currency)
            .WithMany(c => c.InvoiceAmounts)
            .HasForeignKey(ia => ia.CurrencyId);

            builder.Property(x => x.ApprovedAmount).HasColumnType("Decimal(30,5)").HasDefaultValue(0);

            builder.Property(x => x.RequestedAmount).HasColumnType("Decimal(30,5)").HasDefaultValue(0);

            builder.Property(x => x.NettingAmount).HasColumnType("Decimal(30,5)").HasDefaultValue(0);
        }
    }
}
