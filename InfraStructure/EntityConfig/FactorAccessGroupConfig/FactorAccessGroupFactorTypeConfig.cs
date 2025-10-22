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
    public class FactorAccessGroupFactorTypeConfig : IEntityTypeConfiguration<FactorAccessGroupFactorType>
    {
        public void Configure(EntityTypeBuilder<FactorAccessGroupFactorType> builder)
        {
            builder.ToTable("FactorAccessGroupFactorType", "TAM").HasKey(ct => ct.Id);

            builder.HasOne(a => a.FactorAccessGroup).WithMany(k => k.factorAccessGroupFactorTypes).HasForeignKey(a=>a.FactorAccessGroupId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.FactorAccessGroupId);

            builder.HasIndex(a => a.FactorTypeId);
        }
    }
}
