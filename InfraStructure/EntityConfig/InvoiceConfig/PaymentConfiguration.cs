using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceInformations.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.EntityConfig.InvoiceConfig
{
    internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder) {
            // Table Configuration
            builder.ToTable("Payments", "TAM");

            // Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(p => p.PaymentAmount)
            .HasColumnType("decimal(30, 5)")
            .IsRequired();

            builder.Property(npi => npi.ExchangeRate)
            .HasColumnType("decimal(30, 5)")
            .IsRequired();
        }
    }
}
