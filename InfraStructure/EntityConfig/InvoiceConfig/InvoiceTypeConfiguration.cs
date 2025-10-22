using AppCore.Entities.InvoicesInformations.InvoiceType;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.InvoiceConfig
{
    internal class InvoiceTypeConfiguration : IEntityTypeConfiguration<InvoiceType>
    {
        public void Configure(EntityTypeBuilder<InvoiceType> builder) {
            builder.ToTable("InvoiceTypes", "TAM")
            .HasKey(x => x.Id);
        }
    }
}
