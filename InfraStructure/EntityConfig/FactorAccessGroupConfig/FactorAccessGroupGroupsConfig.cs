using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.FactorAccessGroup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.EntityConfig.FactorAccessGroupConfig
{
    public class FactorAccessGroupGroupsConfig : IEntityTypeConfiguration<FactorAccessGroupGroups>
    {
        public void Configure(EntityTypeBuilder<FactorAccessGroupGroups> builder)
        {
            builder.ToTable("FactorAccessGroupGroups", "TAM").HasKey(ct => ct.Id);

            builder.HasOne(a => a.factorAccessGroup).WithMany(k => k.factorAccessGroupGroups).HasForeignKey(x=>x.GroupId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(a => a.AccessGroupParent).WithMany(k => k.FactorAccessGroupGroupChildren).HasForeignKey(a => a.ParentGroupId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(a => a.ParentGroupId);

            builder.HasIndex(a => a.GroupId);
            builder.HasIndex(a => a.ParentGroupId);

        }
    }
}
