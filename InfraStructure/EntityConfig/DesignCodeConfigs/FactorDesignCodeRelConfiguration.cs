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
    internal class FactorDesignCodeRelConfiguration : IEntityTypeConfiguration<FactorDesignCodeParameterRel>
    {
        public void Configure(EntityTypeBuilder<FactorDesignCodeParameterRel> builder)
        {
            builder.ToTable("FactorDesignCodeParameterRel", "TAM");

            builder.HasKey(x => x.Id);

            builder
               .HasOne<FactorDesignCode>()
               .WithMany(fdc => fdc.Parameters)
               .HasForeignKey(p => p.FactorDesignCodeId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
