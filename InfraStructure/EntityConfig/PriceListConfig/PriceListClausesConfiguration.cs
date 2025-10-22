using AppCore.Entities.PriceListEntities.PriceListClauses;
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
    internal class PriceListClausesConfiguration : IEntityTypeConfiguration<PriceListClause>
    {
        public void Configure(EntityTypeBuilder<PriceListClause> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasOne<PriceListField>(pc => pc.PriceListField)
            .WithMany(pl => pl.PriceListClauses)
            .HasForeignKey(pc => pc.PriceListFieldID);
        }
    }
}
