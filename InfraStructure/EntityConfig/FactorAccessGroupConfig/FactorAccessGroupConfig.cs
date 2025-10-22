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
    public class FactorAccessGroupConfig : IEntityTypeConfiguration<FactorAccessGroup>
    {
        public void Configure(EntityTypeBuilder<FactorAccessGroup> builder)
        {
            builder.ToTable("FactorAccessGroup", "TAM").HasKey(x => x.Id);
        }
    }
}
