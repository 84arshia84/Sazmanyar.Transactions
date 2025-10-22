using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupContractTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.EntityConfig.InvoiceAccessGroupConfigs
{
    public class InvoiceAccessGroupContractTypeConfig : IEntityTypeConfiguration<InvoiceAccessGroupContractType>
    {
        public void Configure(EntityTypeBuilder<InvoiceAccessGroupContractType> builder)
        {
            builder.ToTable("InvoiceAccessGroupContractTypes", "TAM").HasKey(ct => ct.Id);
            builder.HasOne(a => a.InvoiceAccessGroup).WithMany(k => k.InvoiceAccessGroupContractTypes).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(a => a.InvoiceAccessGroupId);
            builder.HasIndex(a => a.ContractTypeId);
        }
    }
}
