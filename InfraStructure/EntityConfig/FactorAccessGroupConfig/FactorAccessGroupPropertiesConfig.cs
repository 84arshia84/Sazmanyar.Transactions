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
    public class FactorAccessGroupPropertiesConfig : IEntityTypeConfiguration<FactorAccessGroupProperties>
    {
        public void Configure(EntityTypeBuilder<FactorAccessGroupProperties> builder)
        {
            builder.ToTable("FactorAccessGroupProperties", "TAM").HasKey(u => u.Id);

            builder.HasOne<FactorAccessGroup>(c => c.FactorAccessGroup)
            .WithOne(ct => ct.factorAccessGroupProperties)
            .HasForeignKey<FactorAccessGroupProperties>(c => c.FactorAccessGroupId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.FactorAccessGroupId);

            builder.Property(x => x.FactorTypeView).HasDefaultValue(0);

            builder.Property(x => x.FactorTypeEdit).HasDefaultValue(0);

            builder.Property(x => x.FactorTypeDelete).HasDefaultValue(0);

            builder.Property(x => x.RoleOfOrganizationView).HasDefaultValue(0);

            builder.Property(x => x.RoleOfOrganizationEdit).HasDefaultValue(0);

            builder.Property(x => x.RoleOfOrganizationDelete).HasDefaultValue(0);

            builder.Property(x => x.OrganizationUnitView).HasDefaultValue(0);

            builder.Property(x => x.OrganizationUnitEdit).HasDefaultValue(0);

            builder.Property(x => x.OrganizationUnitDelete).HasDefaultValue(0);
        }
    }
}
