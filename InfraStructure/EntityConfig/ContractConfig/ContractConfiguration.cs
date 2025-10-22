using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using AppCore.Entities.ContractsInformation.ContractCoefficients;
using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.ContractsInformation.ContractFinancialDetailes;
using AppCore.Entities.ContractsInformation.ContractGuarantees;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.Statuses;
using AppCore.Entities.SettingEntities.TransActionTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractConfig
{
    internal class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasOne<ContractTimeProfile>(c => c.ContratTimeProfile)
            .WithOne(ct => ct.Contract)
            .HasForeignKey<Contract>(c => c.TimeProfileID);

            builder.HasOne<ContractFinancialDetails>(c => c.ContractFinancialDetails)
            .WithOne(cf => cf.Contract)
            .HasForeignKey<Contract>(c => c.FinancialDetailsID);

            builder.HasOne<ContractCheckListValue>(c => c.ContractCheckListValues)
           .WithOne(cv => cv.Contract)
           .HasForeignKey<Contract>(c => c.ContractCheckListValueId);

            builder.HasOne<TransActionType>(c => c.TransActionType)
            .WithMany(ta => ta.Contract)
            .HasForeignKey(c => c.TransActionTypeID);

            builder.HasOne<ContractType>(c => c.ContractType)
            .WithMany(ct => ct.Contracts)
            .HasForeignKey(c => c.ContractTypeID);

            builder.HasOne<Organizationalunit>(c => c.Organizationalunit)
            .WithMany(ou => ou.Contract)
            .HasForeignKey(c => c.OrganizationUnitID);

            builder.HasOne<CreditSource>(c => c.CreditSource)
            .WithMany(cs => cs.Contract)
            .HasForeignKey(c => c.CreditSourceID);

            builder.HasOne<Status>(c => c.Status)
            .WithMany(s => s.Contracts)
            .HasForeignKey(c => c.StatusId);

            builder.HasMany<ContractCoefficient>(c => c.ContractCoefficients)
            .WithOne(cc => cc.Contract)
            .HasForeignKey(cc => cc.ContractId);

            builder.HasMany<ContractGuarantee>(c => c.ContractGuarantees)
            .WithOne(cg => cg.Contract)
            .HasForeignKey(cc => cc.ContractId);

            builder.HasMany<ContractAddendum>(c => c.ContractAddendums);

            builder.Property(c=>c.CurrentStatusTitle).HasMaxLength(350);

            builder.Property(c=>c.LastActionTitle).HasMaxLength(350);

            builder.Property(c=>c.IsFinalApprove).HasDefaultValue(false);

        }
    }
}
