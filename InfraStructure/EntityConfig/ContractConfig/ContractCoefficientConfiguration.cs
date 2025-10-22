using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.ContractCoefficients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractConfig
{
    internal class ContractCoefficientConfiguration : IEntityTypeConfiguration<ContractCoefficient>
    {
        public void Configure(EntityTypeBuilder<ContractCoefficient> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("ContractCoefficients", "TAM");

            builder.HasOne<ContractAddendum>(cc => cc.ContractAddendum)
           .WithMany(ca => ca.ContractCoefficients)
           .HasForeignKey(cc => cc.AddendumId);

            builder.Property(x => x.DefaultRowValue)
                .HasMaxLength(50);

            builder.Property(x => x.Title)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.CoefficientValue)
                .HasColumnType("Decimal(10,5)");

            builder.Property(x=>x.IsAddendum).HasDefaultValue(false);

            builder.Property(x => x.UpdatedInAddendum).HasDefaultValue(false);

            builder.Property(x => x.Order).HasDefaultValue(1);

            builder.Ignore(x => x.IsDeleted);
        }
    }
}
