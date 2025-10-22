using AppCore.Entities.FactorInformation.FactorFinancialDetailes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.FactorConfigs
{
    internal class FactorFinancialDetaileConfiguration : IEntityTypeConfiguration<FactorFinancialDetaile>
    {
        public void Configure(EntityTypeBuilder<FactorFinancialDetaile> builder)
        {
            builder.ToTable("FactorFinancialDetailes", "TAM");

            builder.HasKey(fd => fd.Id);

            builder.Property(fd => fd.FactorValue_Added_Percent)
                .HasColumnType("Decimal(8,5)");

            builder.Property(fd => fd.GoodJob_Percent)
                .HasColumnType("Decimal(8,5)");

            builder.Property(fd => fd.FactorTax_Percent)
                .HasColumnType("Decimal(8,5)");

            builder.Property(fd => fd.FactorInsurance_Percent)
                .HasColumnType("Decimal(8,5)");
        }
    }
}
