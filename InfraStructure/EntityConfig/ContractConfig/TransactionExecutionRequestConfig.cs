using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using AppCore.Entities.ContractsInformation.ExecutionRequestCheckListValues;
using AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Entities.SettingEntities.ContractTypes;
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
    internal class TransactionExecutionRequestConfig : IEntityTypeConfiguration<TransactionExecutionRequest>
    {
        public void Configure(EntityTypeBuilder<TransactionExecutionRequest> builder)
        {
            builder.ToTable("TransactionExecutionRequests", "TAM");

            builder.HasKey(t => t.Id);

            builder.HasMany<ExecutionRequestServiceExplanation>(t => t.ExecutionRequestServiceExplanations );

            builder.HasOne<ExecutionRequestCheckListValue>(t => t.ExecutionRequestCheckListValue)
           .WithOne(cv => cv.TransactionExecutionRequests)
           .HasForeignKey<TransactionExecutionRequest>(t => t.ExecutionRequestCheckListValueId);

            builder.HasOne<ContractType>(t => t.ContractType)
            .WithMany(ct => ct.TransactionExecutionRequests)
            .HasForeignKey(t => t.ContractTypeId);

            builder.HasOne<Organizationalunit>(t => t.Organizationalunit)
                .WithMany(or => or.TransactionExecutionRequests)
                .HasForeignKey(t => t.OrganizationId);

            builder.Property(t => t.SubjectOfRequest).HasMaxLength(500);

            builder.Property(t => t.NumberOfRequest).HasMaxLength(100);

            builder.Property(t => t.EstimatedTimeForDoingRequest).HasMaxLength(70);

            builder.Property(c => c.CurrentStatusTitle).HasMaxLength(350);

            builder.Property(c => c.LastActionTitle).HasMaxLength(350);

            builder.Property(c => c.IsFinalApprove).HasDefaultValue(false);

        }
    }
}
