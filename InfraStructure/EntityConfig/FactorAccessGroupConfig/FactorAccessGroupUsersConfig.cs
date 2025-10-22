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
    public class FactorAccessGroupUsersConfig : IEntityTypeConfiguration<FactorAccessGroupUsers>
    {
        public void Configure(EntityTypeBuilder<FactorAccessGroupUsers> builder)
        {
            builder.ToTable("FactorAccessGroupUsers", "TAM").HasKey(u => u.Id);

            builder.HasOne(a => a.factorAccessGroup).WithMany(k => k.factorAccessGroupUsers).HasForeignKey(a=>a.AccessGroupId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.AccessGroupId);
        }
    }
}
