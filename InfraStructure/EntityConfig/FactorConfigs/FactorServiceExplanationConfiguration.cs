using AppCore.Entities.FactorInformation.FactorFinancialDetailes;
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.FactorInformation.FactorServiceExplanations;
using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Entities.SettingEntities.UnitOfMeasurements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.FactorConfigs
{
    internal class FactorServiceExplanationConfiguration : IEntityTypeConfiguration<FactorServiceExplanation>
    {
        public void Configure(EntityTypeBuilder<FactorServiceExplanation> builder)
        {
            builder.ToTable("FactorServiceExplanations", "TAM");

            builder.HasKey(fs => fs.Id);

            builder.HasOne<Factor>(fs => fs.Factor)
             .WithMany(f => f.FactorServiceExplanations)
             .HasForeignKey(fs => fs.FactorId);

            builder.HasOne<Currency>(fs => fs.Currency)
             .WithMany(c => c.FactorServiceExplanations)
             .HasForeignKey(fs => fs.CurrencyID)
             .OnDelete(DeleteBehavior.NoAction);


            builder.Property(fs=>fs.UnitAmount).HasColumnType("Decimal(30,5)");

            builder.Property(fs => fs.TotalAmount).HasColumnType("Decimal(30,5)");

            builder.Property(fs => fs.AccelerationRate).HasColumnType("Decimal(30,5)");

            builder.Property(fs => fs.Amount).HasColumnType("Decimal(30,5)");

            builder.Property(fs => fs.CommodityName).HasMaxLength(400);

            builder.Property(fs => fs.CommodityCode).HasMaxLength(200);

            builder.Property(fs => fs.SupplyListName).HasMaxLength(400);

            

        }
    }
}
