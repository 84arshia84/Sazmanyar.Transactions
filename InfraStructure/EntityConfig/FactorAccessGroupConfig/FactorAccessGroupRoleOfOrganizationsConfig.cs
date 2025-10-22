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
    public class FactorAccessGroupRoleOfOrganizationsConfig : IEntityTypeConfiguration<FactorAccessGroupRoleOfOrganizations>
    {
        public void Configure(EntityTypeBuilder<FactorAccessGroupRoleOfOrganizations> builder)
        {
            builder.ToTable("FactorAccessGroupRoleOfOrganizations", "TAM").HasKey(ct => ct.Id);

            builder.HasOne(a => a.FactorAccessGroup).WithMany(k => k.factorAccessGroupRoleOfOrganizations).HasForeignKey(a=>a.FactorAccessGroupId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.FactorAccessGroupId);

            builder.HasIndex(a => a.RoleOfOrganizationId);
        }
    }
}
