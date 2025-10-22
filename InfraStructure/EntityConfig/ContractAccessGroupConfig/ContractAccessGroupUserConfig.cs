using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractAccessGroupConfig
{
    internal class ContractAccessGroupUserConfig : IEntityTypeConfiguration<ContractAccessGroupUsers>
    {
        public void Configure(EntityTypeBuilder<ContractAccessGroupUsers> builder)
        {
            builder.ToTable("ContractAccessGroupUsers", "TAM").HasKey(u => u.Id);

            builder.HasOne(a => a.AccessGroup).WithMany(k => k.ContractAccessGroupUsers).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.AccessGroupId);
        }
    }
}
