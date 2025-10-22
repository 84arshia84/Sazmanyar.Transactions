using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using AppCore.Entities.ContractsInformation.ExecutionRequestCheckListValues;
using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractConfig
{
    internal class ExecutionRequestCheckListValueCconfig : IEntityTypeConfiguration<ExecutionRequestCheckListValue>
    {
        public void Configure(EntityTypeBuilder<ExecutionRequestCheckListValue> builder)
        {
            builder.ToTable("ExecutionRequestCheckListValue", "TAM");
            builder.HasKey(x => x.Id);

            builder.HasOne<TransactionExecutionRequest>(cv => cv.TransactionExecutionRequests)
           .WithOne(t => t.ExecutionRequestCheckListValue)
           .HasForeignKey<ExecutionRequestCheckListValue>(cv => cv.TransactionExecutionRequestsId);
        }
    }
}
