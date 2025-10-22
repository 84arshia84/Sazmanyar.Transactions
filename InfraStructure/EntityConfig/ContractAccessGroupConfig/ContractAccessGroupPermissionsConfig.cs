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
    internal class ContractAccessGroupPermissionsConfig : IEntityTypeConfiguration<ContractAccessGroupPermissions>
    {
        public void Configure(EntityTypeBuilder<ContractAccessGroupPermissions> builder)
        {
            builder.ToTable("ContractAccessGroupPermissions", "TAM").HasKey(u => u.Id);

            builder.HasOne<ContractAccessGroup>(c => c.ContractAccessGroup)
            .WithOne(ct => ct.ContractAccessGroupPermissions)
            .HasForeignKey<ContractAccessGroupPermissions>(c => c.ContractAccessGroupId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.ContractAccessGroupId);

            builder.Property(x=>x.PriceListSettings).HasDefaultValue(0);

            builder.Property(x => x.AccessGroupSettings).HasDefaultValue(0);

            builder.Property(x => x.BaseSettings).HasDefaultValue(0);

            builder.Property(x => x.Contract).HasDefaultValue(0);

            builder.Property(x => x.ContractAddendum).HasDefaultValue(0);

            builder.Property(x => x.WorkFlow).HasDefaultValue(0);


        }
    }
}
