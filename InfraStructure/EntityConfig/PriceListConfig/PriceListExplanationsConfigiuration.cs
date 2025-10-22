using AppCore.Entities.PriceListEntities.PriceListClauses;
using AppCore.Entities.PriceListEntities.PriceListFields;
using AppCore.Entities.PriceListEntities.PriceListExplanations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.PriceListConfig
{
    internal class PriceListExplanationsConfigiuration : IEntityTypeConfiguration<PriceListExplanation>
    {
        public void Configure(EntityTypeBuilder<PriceListExplanation> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasOne<PriceListClause>(pe => pe.PriceListClause)
            .WithMany(pc => pc.PriceListExplanations)
            .HasForeignKey(pe => pe.PriceListClauseID);
        }
    }
}
