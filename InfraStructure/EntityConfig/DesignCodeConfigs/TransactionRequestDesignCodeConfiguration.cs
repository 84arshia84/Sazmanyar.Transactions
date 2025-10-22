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
    internal class TransactionRequestDesignCodeConfiguration : IEntityTypeConfiguration<TransActionExecutionDesignCode>
    {
        public void Configure(EntityTypeBuilder<TransActionExecutionDesignCode> builder)
        {
            builder.ToTable("TransActionExecutionDesignCode", "TAM");

            builder.HasKey(x => x.Id);

            builder.Property(dc => dc.Counter).HasDefaultValue(0);
        }
    }
}
