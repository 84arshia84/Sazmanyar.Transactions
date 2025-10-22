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
    internal class ContractAccessGroupOrganizationUnitConfig : IEntityTypeConfiguration<ContractAccessGroupOrganizationUnits>
    {
        public void Configure(EntityTypeBuilder<ContractAccessGroupOrganizationUnits> builder)
        {
            builder.ToTable("ContractAccessGroupOrganizationUnits", "TAM").HasKey(ct => ct.Id);

            builder.HasOne(a => a.ContractAccessGroup).WithMany(k => k.ContractAccessGroupOrganizationUnits)
                .HasForeignKey(a => a.ContractAccessGroupId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.OrganizationalUnit).WithMany(i => i.ContractAccessGroupOrganizationUnits)
                .HasForeignKey(o => o.OrganizationUnitId);

            builder.HasIndex(a => a.ContractAccessGroupId);

            builder.HasIndex(a => a.OrganizationUnitId);
        }
    }
}
