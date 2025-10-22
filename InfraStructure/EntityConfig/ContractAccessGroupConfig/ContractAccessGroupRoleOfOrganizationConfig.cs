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
    internal class ContractAccessGroupRoleOfOrganizationConfig : IEntityTypeConfiguration<ContractAccessGroupRoleOfOrganizations>
    {
        public void Configure(EntityTypeBuilder<ContractAccessGroupRoleOfOrganizations> builder)
        {
            builder.ToTable("ContractAccessGroupRoleOfOrganizations", "TAM").HasKey(ct => ct.Id);

            builder.HasOne(a => a.ContractAccessGroup).WithMany(k => k.ContractAccessGroupRoleOfOrganizations).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.ContractAccessGroupId);

            builder.HasIndex(a => a.RoleOfOrganizationId);
        }
    }
}
