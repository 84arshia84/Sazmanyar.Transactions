using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.EntityConfig.InvoiceAccessGroupConfigs
{
    public class InvoiceAccessGroupConfig : IEntityTypeConfiguration<InvoiceAccessGroup>
    {
        public void Configure(EntityTypeBuilder<InvoiceAccessGroup> builder)
        {
            builder.ToTable("InvoiceAccessGroups", "TAM").HasKey(u => u.Id);
        }
    }
}
