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
    public class FactorAccessGroupOrganizationUnitsConfig : IEntityTypeConfiguration<FactorAccessGroupOrganizationUnits>
    {
        public void Configure(EntityTypeBuilder<FactorAccessGroupOrganizationUnits> builder)
        {
            builder.ToTable("FactorAccessGroupOrganizationUnits", "TAM").HasKey(ct => ct.Id);

            builder.HasOne(a => a.FactorAccessGroup).WithMany(k => k.factorAccessGroupOrganizationUnits)
                .HasForeignKey(a => a.FactorAccessGroupId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.OrganizationalUnit).WithMany(i => i.FactorAccessGroupOrganizationUnits)
                .HasForeignKey(o => o.OrganizationUnitId);

            builder.HasIndex(a => a.FactorAccessGroupId);

            builder.HasIndex(a => a.OrganizationUnitId);
        }
    }
}
