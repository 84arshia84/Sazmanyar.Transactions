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
    internal class ContractDesignCodeConfiguration : IEntityTypeConfiguration<ContractDesignCode>
    {
        public void Configure(EntityTypeBuilder<ContractDesignCode> builder)
        {
            builder.ToTable("ContractDesignCode", "TAM");

            builder.HasKey(x => x.Id);

            builder.Property(dc => dc.Counter).HasDefaultValue(0);

        }
    }
}
