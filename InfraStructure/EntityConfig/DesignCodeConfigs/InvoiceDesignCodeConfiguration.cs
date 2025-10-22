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
    internal class InvoiceDesignCodeConfiguration : IEntityTypeConfiguration<InvoiceDesignCode>
    {
        public void Configure(EntityTypeBuilder<InvoiceDesignCode> builder)
        {
            builder.ToTable("InvoiceDesignCode", "TAM");

            builder.HasKey(x => x.Id);

            builder.Property(dc => dc.Counter).HasDefaultValue(0);
        }
    }
}
