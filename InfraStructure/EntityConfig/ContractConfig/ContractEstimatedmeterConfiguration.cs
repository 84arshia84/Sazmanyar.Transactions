using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractConfig
{
    internal class ContractEstimatedmeterConfiguration : IEntityTypeConfiguration<ContractEstimatedmeter>
    {
        public void Configure(EntityTypeBuilder<ContractEstimatedmeter> builder)
        {
            builder.Property(x => x.Amount)
                .HasColumnType("Decimal(30,5)");

            builder.Property(x => x.RowPrice)
               .HasColumnType("Decimal(30,5)");

            builder.Property(x => x.RawPrice)
               .HasColumnType("Decimal(30,5)");

            builder.Property(x => x.SumOfCoefficients)
               .HasColumnType("Decimal(10,5)");

            builder.Property(x => x.UpdatedInAddendum).HasDefaultValue(false);

            builder.Property(x => x.IsAddendum).HasDefaultValue(false);

            builder.Property(x => x.Order).HasDefaultValue(1);

            builder.HasOne(x => x.PriceList)
                .WithMany(x => x.ContractEstimatedmeters)
                .HasForeignKey(x => x.YearId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PriceListClause)
                .WithMany(x => x.ContractEstimatedmeters)
                .HasForeignKey(x => x.ClauseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PriceListExplanation)
                .WithMany(x => x.ContractEstimatedmeters)
                .HasForeignKey(x => x.ExplenationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PriceListField)
                .WithMany(x => x.ContractEstimatedmeters)
                .HasForeignKey(x => x.FieldId)
                .OnDelete(DeleteBehavior.NoAction);

           

        }
    }
}
