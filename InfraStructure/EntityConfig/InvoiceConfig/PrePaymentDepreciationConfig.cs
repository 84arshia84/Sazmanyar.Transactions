using AppCore.Entities.InvoiceInformations.PrePaymentDepreciations;
using AppCore.Entities.SettingEntities.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.InvoiceConfig
{
    internal class PrePaymentDepreciationConfig : IEntityTypeConfiguration<PrePaymentDepreciation>
    {
        public void Configure(EntityTypeBuilder<PrePaymentDepreciation> builder)
        {
            builder.ToTable("PrePaymentDepreciations", "TAM");

            builder.HasKey(x => x.Id);

            builder.HasOne<Currency>(n => n.Currency)
             .WithMany(c => c.PrePaymentDepreciations)
             .HasForeignKey(n => n.CurrencyId);

            builder.Property(x => x.SuggestedDepreciationAmount).HasColumnType("decimal(30, 5)").HasDefaultValue(0);

            builder.Property(x => x.ApprovedDepreciationAmount).HasColumnType("decimal(30, 5)").HasDefaultValue(0);

            builder.Property(x=>x.IsDeleted).HasDefaultValue(false);

        }
    }
}
