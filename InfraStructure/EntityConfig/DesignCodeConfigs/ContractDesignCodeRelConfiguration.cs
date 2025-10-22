using AppCore.Entities.DesignCodesProperties.ContractDesignCodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.DesignCodeConfigs
{
    internal class ContractDesignCodeRelConfiguration : IEntityTypeConfiguration<ContractDesignCodeParameterRel>
    {
        public void Configure(EntityTypeBuilder<ContractDesignCodeParameterRel> builder)
        {
            builder.ToTable("ContractDesignCodeParameterRel", "TAM");

            builder.HasKey(x => x.Id);

            builder
               .HasOne<ContractDesignCode>()
               .WithMany(cdc => cdc.Parameters)
               .HasForeignKey(p => p.ContractDesignCodeId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
