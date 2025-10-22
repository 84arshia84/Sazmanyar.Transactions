using AppCore.Entities.InvoiceInformations.InvoiceBaseCustomization;
using AppCore.Entities.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.InvoiceBaseCusomizationConfig
{
    internal class InvoiceBaseCustomizationConfiguration : IEntityTypeConfiguration<InvoiceBaseCustomization>
    {
        public void Configure(EntityTypeBuilder<InvoiceBaseCustomization> builder)
        {
            builder.HasKey(c => c.Id);

        }
    }
}
