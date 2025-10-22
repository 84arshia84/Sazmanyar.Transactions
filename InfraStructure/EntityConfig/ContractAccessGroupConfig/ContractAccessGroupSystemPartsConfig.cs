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
    internal class ContractAccessGroupSystemPartsConfig : IEntityTypeConfiguration<ContractAccessGroupSystemParts>
    {
        public void Configure(EntityTypeBuilder<ContractAccessGroupSystemParts> builder)
        {
            builder.ToTable("ContractAccessGroupSystemParts", "TAM").HasKey(x => x.Id);

            builder.HasOne(a => a.ContractAccessGroup).WithMany(k => k.ContractAccessGroupSystemParts).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.ContractAccessGroupId);

            builder.HasIndex(a => a.Id);
        }
    }
}
