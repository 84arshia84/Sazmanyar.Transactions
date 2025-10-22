using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.ContractCoefficients;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.SettingEntities.AddendumTypes;
using AppCore.Entities.SettingEntities.Organizationalunits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractConfig
{
    internal class ContractAddendumConfiguration : IEntityTypeConfiguration<ContractAddendum>
    {
        public void Configure(EntityTypeBuilder<ContractAddendum> builder)
        {
            builder.ToTable("ContractAddendums", "TAM");

            builder.HasKey(x => x.Id);

            builder.HasOne<Contract>(ca => ca.Contract)
            .WithMany(c => c.ContractAddendums)
            .HasForeignKey(ca => ca.ContractId)
            .OnDelete(DeleteBehavior.NoAction); 

            builder.HasOne<AddendumType>(ca => ca.AddendumType)
            .WithMany(c => c.ContractAddendum)
            .HasForeignKey(ca => ca.AddendumTypeId)
            .OnDelete(DeleteBehavior.NoAction); 

            builder.HasMany<ServiceExplanation>(ca => ca.ServiceExplanations);

            builder.HasMany<ContractCoefficient>(ca => ca.ContractCoefficients);

            builder.Property(c => c.CurrentStatusTitle).HasMaxLength(350);

            builder.Property(c => c.LastActionTitle).HasMaxLength(350);

            builder.Property(c => c.IsFinalApprove).HasDefaultValue(false);
        }
    }
}
