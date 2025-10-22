using AppCore.Entities.ContractsInformation.ContractFinancialDetailes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractConfig
{
    internal class ContractFinancialDetailsConfiguration : IEntityTypeConfiguration<ContractFinancialDetails>
    {
        public void Configure(EntityTypeBuilder<ContractFinancialDetails> builder)
        {
            builder.Property(x => x.ContractValue_Added_Percent)
                .HasColumnType("Decimal(8,5)");

            builder.Property(x => x.GoodJob_Percent)
                .HasColumnType("Decimal(8,5)");

            builder.Property(x => x.ContractTax_Percent)
                .HasColumnType("Decimal(8,5)");

            builder.Property(x => x.ContractInsurance_Percent)
                .HasColumnType("Decimal(8,5)");

            builder.Property(x => x.PercentageOfChanges)
                .HasColumnType("Decimal(8,5)");
        }
    }
}
