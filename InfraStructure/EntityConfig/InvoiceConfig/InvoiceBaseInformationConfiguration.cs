using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.InvoiceConfig
{
    internal class InvoiceBaseInformationConfiguration : IEntityTypeConfiguration<InvoiceBaseInformation>
    {
        public void Configure(EntityTypeBuilder<InvoiceBaseInformation> builder)
        {
            // Table Configuration
            builder.ToTable("InvoiceBaseInformations", "TAM");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.InvoiceTitle)
                   .IsRequired()
                   .HasMaxLength(250); // Example: Setting a max length constraint

            builder.Property(x => x.LeadingToDate);

            builder.Property(x => x.SendDate)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(1000); // Example: Optional with max length

            builder.Property(x => x.InvoiceCode)
                   .HasMaxLength(50);

            builder.Property(x => x.InvoiceNumber)
                   .IsRequired(false); // Nullable property

            builder.Property(x => x.InsertDate)
                   .IsRequired();

            builder.Property(x => x.InsertBy)
                   .IsRequired();

            builder.Property(x => x.DeleteBy)
                   .IsRequired(false); // Nullable property

            builder.Property(x => x.DeleteDate)
                   .IsRequired(false); // Nullable property

            builder.Property(x => x.IsDeleted)
                   .IsRequired();


            // Relationships
            builder.HasOne(x => x.Contract)
                   .WithMany(c => c.InvoiceBaseInformations) // Establish two-way relationship
                   .HasForeignKey(x => x.ContractId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            builder.HasOne(x => x.InvoiceType)
                   .WithMany()
                   .HasForeignKey(x => x.InvoiceTypeId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            builder.HasOne<Account>(ib => ib.Account)
                .WithMany(ac => ac.InvoiceBaseInformation)
                .HasForeignKey(ib => ib.AccountId);
        }
    }
}
