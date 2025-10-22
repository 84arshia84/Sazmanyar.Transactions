using AppCore.Entities.InvoiceInformations.EstimatedMeterFinancials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.InvoiceConfig
{
    internal class EstimatedMeterFinancialConfiguration : IEntityTypeConfiguration<EstimatedMeterFinancial>
    {
        public void Configure(EntityTypeBuilder<EstimatedMeterFinancial> builder)
        {
            // Table Configuration
            builder.ToTable("EstimatedMeterFinancials", "TAM");

            // Primary Key
            builder.HasKey(emf => emf.Id);

            builder.Property(emf => emf.RequestedVolume)
                .HasColumnType("decimal(30, 5)");

            builder.Property(emf => emf.RequestedPercent)
                .HasColumnType("decimal(7, 5)")
                .IsRequired();

            builder.Property(emf => emf.RequestedPrice)
                .HasColumnType("decimal(30, 5)")
                .IsRequired();

            builder.Property(emf => emf.ApprovedVolume)
                .HasColumnType("decimal(30, 5)");

            builder.Property(emf => emf.ApprovedPercent)
                .HasColumnType("decimal(7, 5)")
                .IsRequired();

            builder.Property(emf => emf.ApprovedPrice)
                .HasColumnType("decimal(30, 5)")
                .IsRequired();

            builder.HasOne(emf => emf.InvoiceBaseInformation)
                .WithMany(ibi => ibi.EstimatedMeterFinancials)
                .HasForeignKey(emf => emf.InvoiceBaseInformationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
