using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.InvoiceConfig
{
    internal class ServiceExplanationFinancialConfiguration : IEntityTypeConfiguration<ServiceExplanationFinancial>
    {
        public void Configure(EntityTypeBuilder<ServiceExplanationFinancial> builder)
        {
            // Table Configuration
            builder.ToTable("ServiceExplanationFinancials", "TAM");

            // Primary Key
            builder.HasKey(sef => sef.Id);

            builder.Property(sef => sef.RequestedVolume)
                .HasColumnType("decimal(30, 5)");

            builder.Property(sef => sef.RequestedPercent)
                .HasColumnType("decimal(8, 5)")
                .IsRequired();

            builder.Property(sef => sef.RequestedPrice)
                .HasColumnType("decimal(30, 5)")
                .IsRequired();

            builder.Property(sef => sef.ApprovedVolume)
                .HasColumnType("decimal(30, 5)");

            builder.Property(sef => sef.ApprovedPercent)
                .HasColumnType("decimal(8, 5)")
                .IsRequired();

            builder.Property(sef => sef.ApprovedPrice)
                .HasColumnType("decimal(30, 5)")
                .IsRequired();

            builder.HasOne(sef => sef.InvoiceBaseInformation)
                .WithMany(ibi => ibi.ServiceExplanationFinancials)
                .HasForeignKey(sef => sef.InvoiceBaseInformationId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
