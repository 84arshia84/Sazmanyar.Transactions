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
    public class FactorAccessGroupPermissionsConfig : IEntityTypeConfiguration<FactorAccessGroupPermissions>
    {
        public void Configure(EntityTypeBuilder<FactorAccessGroupPermissions> builder)
        {
            builder.ToTable("FactorAccessGroupPermissions", "TAM").HasKey(u => u.Id);

            builder.HasOne<FactorAccessGroup>(c => c.FactorAccessGroup)
            .WithOne(ct => ct.factorAccessGroupPermissions)
            .HasForeignKey<FactorAccessGroupPermissions>(c => c.FactorAccessGroupId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.FactorAccessGroupId);
            builder.Property(x => x.AccessGroupSettings).HasDefaultValue(0);

            builder.Property(x => x.BaseSettings).HasDefaultValue(0);

            builder.Property(x => x.Factor).HasDefaultValue(0);


            builder.Property(x => x.WorkFlow).HasDefaultValue(0);
        }
    }
}
