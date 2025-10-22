using AppCore.Entities.DesignCodesProperties.FactorDesignCodes;
using AppCore.Entities.DesignCodesProperties.InvoiceDesignCodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.DesignCodeConfigs
{
    internal class InvoiceDesignCodeRelConfiguration : IEntityTypeConfiguration<InvoiceDesignCodeParameterRel>
    {
        public void Configure(EntityTypeBuilder<InvoiceDesignCodeParameterRel> builder)
        {
            builder.ToTable("InvoiceDesignCodeParameterRel", "TAM");

            builder.HasKey(x => x.Id);

            builder
               .HasOne<InvoiceDesignCode>()
               .WithMany(idc => idc.Parameters)
               .HasForeignKey(p => p.InvoiceDesignCodeId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
