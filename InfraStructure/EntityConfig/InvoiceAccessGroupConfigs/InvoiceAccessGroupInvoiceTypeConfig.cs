using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupInvoiceTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.EntityConfig.InvoiceAccessGroupConfigs
{
    public class InvoiceAccessGroupInvoiceTypeConfig : IEntityTypeConfiguration<InvoiceAccessGroupInvoiceType>
    {
        public void Configure(EntityTypeBuilder<InvoiceAccessGroupInvoiceType> builder)
        {
            builder.ToTable("InvoiceAccessGroupInvoiceTypes", "TAM").HasKey(ct => ct.Id);
            builder.HasOne(a => a.InvoiceAccessGroup).WithMany(k => k.InvoiceAccessGroupInvoiceTypes).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(a => a.InvoiceAccessGroupId);
            builder.HasIndex(a => a.InvoiceTypeId);
        }
    }
}
