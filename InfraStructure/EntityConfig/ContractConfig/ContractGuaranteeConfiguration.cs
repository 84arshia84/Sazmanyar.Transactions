using AppCore.Entities.ContractsInformation.ContractGuarantees;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.Entities.SettingEntities.ForGuarantees;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.ReleaseConditions;
using AppCore.Entities.SettingEntities.TypeOfGuarantees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractConfig
{
    internal class ContractGuaranteeConfiguration : IEntityTypeConfiguration<ContractGuarantee>
    {
        public void Configure(EntityTypeBuilder<ContractGuarantee> builder)
        {
            builder.ToTable("ContractGuarantees", "TAM");

            builder.HasKey(x => x.Id);

            builder.HasOne<ForGuarantee>(cg => cg.ForGuarantee)
           .WithMany(fg => fg.ContractGuarantees)
           .HasForeignKey(cg => cg.ForGuaranteeId);

            builder.HasOne<ReleaseCondition>(cg => cg.ReleaseCondition)
           .WithMany(fg => fg.ContractGuarantees)
           .HasForeignKey(cg => cg.ReleaseConditionId);

            builder.HasOne<TypeOfGuarantee>(cg => cg.TypeOfGuarantee)
           .WithMany(fg => fg.ContractGuarantees)
           .HasForeignKey(cg => cg.TypeOfGuaranteeId);

            builder.HasOne<Contract>(cg => cg.Contract)
           .WithMany(fg => fg.ContractGuarantees)
           .HasForeignKey(cg => cg.ContractId);

            builder.Property(x => x.GuaranteePrice)
                .HasColumnType("Decimal(30,5)");

            builder.Ignore(cg => cg.IsUpdate);
            builder.Ignore(cg => cg.IsDeleted);
        }
    }
}
