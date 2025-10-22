using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.EntityConfig.InvoiceAccessGroupConfigs
{
    public class InvoiceAccessGroupPropertiesConfig : IEntityTypeConfiguration<InvoiceAccessGroupProperties>
    {
        public void Configure(EntityTypeBuilder<InvoiceAccessGroupProperties> builder)
        {
            builder.ToTable("InvoiceAccessGroupProperties", "TAM").HasKey(u => u.Id);
            builder.HasOne(a => a.InvoiceAccessGroup).WithMany(k => k.InvoiceAccessGroupProperties).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(a => a.InvoiceAccessGroupId);
        }
    }
}
