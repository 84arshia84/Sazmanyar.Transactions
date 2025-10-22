using AppCore.Entities.InvoiceInformations.NettingProcesses;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.InvoiceConfig
{
    internal class NettingProcessItemConfiguration : IEntityTypeConfiguration<NettingProcessItem>
    {
        public void Configure(EntityTypeBuilder<NettingProcessItem> builder)
        {
            // Table Configuration
            builder.ToTable("NettingProcessItems", "TAM");

            // Primary Key
            builder.HasKey(x => x.Id);

            builder.HasOne<Currency>(n => n.Currency)
             .WithMany(c => c.InvoiceNettingProcessItems)
             .HasForeignKey(n => n.CurrencyId);

            // Properties
            builder.Property(npi => npi.Percentage)
            .HasColumnType("decimal(7, 5)")
            .IsRequired();

            builder.Property(npi => npi.Amount)
            .HasColumnType("decimal(30, 5)")
            .IsRequired();

            builder.HasOne(npi => npi.InvoiceBaseInformation)
                .WithMany(ibi => ibi.NettingProcessItems)
                .HasForeignKey(npi => npi.InvoiceBaseInformationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(npi => npi.NettingProcessTypes).HasDefaultValue(NettingProcessTypesEnum.others);
        }
    }
}
