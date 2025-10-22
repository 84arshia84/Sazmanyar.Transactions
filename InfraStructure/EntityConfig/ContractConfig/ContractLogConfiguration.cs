using AppCore.Entities.ContractsInformation.ContractLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractConfig
{
    internal class ContractLogConfiguration : IEntityTypeConfiguration<ContractLog>
    {
        public void Configure(EntityTypeBuilder<ContractLog> builder)
        {
            builder.ToTable("ContractLogs", "TAM");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UpdatedBy).HasMaxLength(200).IsRequired();
            builder.Property(x => x.ChangesSummary).HasColumnType("nvarchar(max)");

            // FK relation to Contract (optional navigation if desired)
            builder.HasIndex(x => x.ContractId);
            builder.HasOne<AppCore.Entities.ContractsInformation.Contratcs.Contract>()
                   .WithMany() // no navigation required on Contract side
                   .HasForeignKey(x => x.ContractId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
