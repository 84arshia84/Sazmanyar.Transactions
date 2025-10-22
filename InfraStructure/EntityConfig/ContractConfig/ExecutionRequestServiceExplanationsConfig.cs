using AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations;
using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Entities.SettingEntities.FinePaymentMethods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractConfig
{
    internal class ExecutionRequestServiceExplanationsConfig : IEntityTypeConfiguration<ExecutionRequestServiceExplanation>
    {
        public void Configure(EntityTypeBuilder<ExecutionRequestServiceExplanation> builder)
        {
            builder.ToTable("ExecutionRequestServiceExplanation", "TAM");

            builder.HasKey(t => t.ID);

            builder.HasOne<TransactionExecutionRequest>(es => es.TransactionExecutionRequest)
            .WithMany(t => t.ExecutionRequestServiceExplanations)
            .HasForeignKey(es => es.TransactionExecutionRequestId);

            builder.HasOne<FinePaymentMethod>(S => S.FinePaymentMethod)
           .WithMany(ac => ac.ExecutionRequestServiceExplanations)
           .HasForeignKey(S => S.FinePaymentMethodID);

            builder.HasOne<Currency>(S => S.Currency)
           .WithMany(ac => ac.ExecutionRequestServiceExplanations)
           .HasForeignKey(S => S.CurrencyID);

            builder.Property(x => x.CommodityName).HasMaxLength(100);

            builder.Property(x => x.SupplyListName).HasMaxLength(100);

            builder.Property(x=>x.ProposalName).HasMaxLength(250);

            builder.Property(x=>x.ProjectName).HasMaxLength(250);

            builder.Property(x=>x.ActivityCenterTitle).HasMaxLength(250);

            builder.Property(x => x.PrepaymentPercentage)
                .HasColumnType("Decimal(7,5)");

            builder.Property(x => x.AccelerationRate)
                .HasColumnType("Decimal(10,5)");

            builder.Property(x => x.ProgramVolume)
                .HasColumnType("Decimal(30,5)");

            builder.Property(x => x.UnitAmount)
                .HasColumnType("Decimal(30,5)");

            builder.Property(x => x.TotalAmount)
                .HasColumnType("Decimal(30,5)");

            builder.Ignore(x => x.IsDeleted);
        }
    }
}
