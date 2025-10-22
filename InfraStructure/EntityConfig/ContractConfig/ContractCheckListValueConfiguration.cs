using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using AppCore.Entities.ContractsInformation.ContractFinancialDetailes;
using AppCore.Entities.ContractsInformation.Contratcs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractConfig
{
    internal class ContractCheckListValueConfiguration : IEntityTypeConfiguration<ContractCheckListValue>
    {
        public void Configure(EntityTypeBuilder<ContractCheckListValue> builder)
        {
            builder.ToTable("ContractCheckListValues", "TAM");

            builder.HasKey(x => x.Id);

            builder.HasOne<Contract>(cv => cv.Contract)
           .WithOne(cf => cf.ContractCheckListValues)
           .HasForeignKey<ContractCheckListValue>(cv => cv.ContractId);
            

        }
    }
}
