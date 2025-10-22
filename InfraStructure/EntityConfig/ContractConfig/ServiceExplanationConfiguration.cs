using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Entities.SettingEntities.FinePaymentMethods;
using AppCore.Entities.SettingEntities.UnitOfMeasurements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractConfig
{
    internal class ServiceExplanationConfiguration : IEntityTypeConfiguration<ServiceExplanation>
    {
        public void Configure(EntityTypeBuilder<ServiceExplanation> builder)
        {
            builder.HasKey(x => x.ID);

           // builder.HasOne<Activitycenter>(S => S.Activitycenter)
           //.WithMany(ac => ac.ServiceExplanation)
           //.HasForeignKey(S => S.ActivityCenterID);

            builder.HasOne<Currency>(S => S.Currency)
           .WithMany(ac => ac.ServiceExplanation)
           .HasForeignKey(S => S.CurrencyID);

            builder.HasOne<FinePaymentMethod>(S => S.FinePaymentMethod)
           .WithMany(ac => ac.ServiceExplanation)
           .HasForeignKey(S => S.FinePaymentMethodID);

            builder.HasOne<Contract>(S => S.Contract)
           .WithMany(c => c.ServiceExplanations)
           .HasForeignKey(S => S.ContractID);

            builder.HasMany<ContractEstimatedmeter>(s => s.ContractEstimatedmeters)
           .WithOne(ce => ce.ServiceExplanation)
           .HasForeignKey(ce => ce.ServiceExplanationId);

            builder.HasOne<ContractAddendum>(S => S.ContractAddendum)
           .WithMany(ca => ca.ServiceExplanations)
           .HasForeignKey(S => S.ContractAddendumId)
           .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.UpdatedInAddendum).HasDefaultValue(false);

            builder.Property(x => x.PrepaymentPercentage)
                .HasColumnType("Decimal(7,5)");

            builder.Property(x => x.AccelerationRate)
                .HasColumnType("Decimal(30,5)");

            builder.Property(x => x.ProgramVolume)
                .HasColumnType("Decimal(30,5)");

            builder.Property(x => x.UnitAmount)
                .HasColumnType("Decimal(30,5)");

            builder.Property(x => x.TotalAmount)
                .HasColumnType("Decimal(30,5)");

            builder.Property(x => x.DiscountAmount)
                .HasColumnType("Decimal(30,5)")
                .HasDefaultValue(0);


        }
    }
}
