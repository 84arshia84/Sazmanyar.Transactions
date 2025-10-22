using AppCore.Entities.DesignCodesProperties.FactorDesignCodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.DesignCodeConfigs
{
    internal class FactorDesignCodeConfiguration : IEntityTypeConfiguration<FactorDesignCode>
    {
        public void Configure(EntityTypeBuilder<FactorDesignCode> builder)
        {
            builder.ToTable("FactorDesignCode", "TAM");

            builder.HasKey(x => x.Id);

            builder.Property(dc => dc.Counter).HasDefaultValue(0);
        }
    }
}
