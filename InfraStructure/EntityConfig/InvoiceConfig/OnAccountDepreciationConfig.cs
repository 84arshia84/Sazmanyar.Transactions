using AppCore.Entities.InvoiceInformations.OnAccountDepreciations;
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
    internal class OnAccountDepreciationConfig : IEntityTypeConfiguration<OnAccountDepreciation>
    {
        public void Configure(EntityTypeBuilder<OnAccountDepreciation> builder)
        {
            builder.ToTable("OnAccountDepreciation", "TAM");

            builder.HasKey(x => x.Id);

            builder.HasOne<Currency>(n => n.Currency)
             .WithMany(c => c.OnAccountDepreciations)
             .HasForeignKey(n => n.CurrencyId);

            builder.Property(x => x.SuggestedDepreciationAmount).HasColumnType("decimal(30, 5)").HasDefaultValue(0);

            builder.Property(x => x.ApprovedDepreciationAmount).HasColumnType("decimal(30, 5)").HasDefaultValue(0);

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}
