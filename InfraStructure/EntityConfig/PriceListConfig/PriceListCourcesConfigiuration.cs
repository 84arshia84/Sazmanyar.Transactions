using AppCore.Entities.PriceListEntities.PriceListFields;
using AppCore.Entities.PriceListEntities.PriceLists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.PriceListConfig
{
    internal class PriceListCourcesConfigiuration : IEntityTypeConfiguration<PriceListField>
    {
        public void Configure(EntityTypeBuilder<PriceListField> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasOne<PriceList>(p => p.PriceList)
            .WithMany(pl => pl.PriceListFields)
            .HasForeignKey(p => p.PriceListID);
        }
    }
}
