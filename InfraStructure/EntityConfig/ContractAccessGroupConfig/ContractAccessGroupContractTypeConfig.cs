using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupContractTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractAccessGroupConfig
{
    internal class ContractAccessGroupContractTypeConfig : IEntityTypeConfiguration<ContractAccessGroupContractType>
    {
        public void Configure(EntityTypeBuilder<ContractAccessGroupContractType> builder)
        {
            builder.ToTable("ContractAccessGroupContractTypes", "TAM").HasKey(ct => ct.Id);

            builder.HasOne(a => a.ContractAccessGroup).WithMany(k => k.ContractAccessGroupContractTypes).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.ContractAccessGroupId);

            builder.HasIndex(a => a.ContractTypeId);
        }
    }
}
