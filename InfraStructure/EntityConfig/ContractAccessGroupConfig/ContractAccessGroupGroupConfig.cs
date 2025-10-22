using AppCore.Entities.ContractAccessGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractAccessGroupConfig
{
    internal class ContractAccessGroupGroupConfig : IEntityTypeConfiguration<ContractAccessGroupGroups>
    {
        public void Configure(EntityTypeBuilder<ContractAccessGroupGroups> builder)
        {
            builder.ToTable("ContractAccessGroupGroups", "TAM").HasKey(u => u.Id);

            builder.HasOne(a => a.AccessGroup).WithMany(k => k.ContractAccessGroupGroups).HasForeignKey(a => a.GroupId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.AccessGroupParent).WithMany(k => k.ContractAccessGroupGroupChildren).HasForeignKey(a => a.ParentGroupId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.ParentGroupId);

            builder.HasIndex(a => a.GroupId);
        }
    }
}
