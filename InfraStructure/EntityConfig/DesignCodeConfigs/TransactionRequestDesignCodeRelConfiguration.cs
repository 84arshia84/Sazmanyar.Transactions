using AppCore.Entities.DesignCodesProperties.InvoiceDesignCodes;
using AppCore.Entities.DesignCodesProperties.TransActionExecutionDesignCodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.DesignCodeConfigs
{
    internal class TransactionRequestDesignCodeRelConfiguration : IEntityTypeConfiguration<TransActionExecutionDesignCodeParameterRel>
    {
        public void Configure(EntityTypeBuilder<TransActionExecutionDesignCodeParameterRel> builder)
        {
            builder.ToTable("TransActionExecutionDesignCodeParameterRel", "TAM");

            builder.HasKey(x => x.Id);

            builder
               .HasOne<TransActionExecutionDesignCode>()
               .WithMany(idc => idc.Parameters)
               .HasForeignKey(p => p.TransactionExecutionDesignCodeId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
