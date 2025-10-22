using AppCore.Entities.Descriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.DescriptionConfig
{
    internal class DescriptionConfiguration : IEntityTypeConfiguration<Description>
    {
        public void Configure(EntityTypeBuilder<Description> builder)
        {
            builder.ToTable("Description", "TAM");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AuthorName).HasMaxLength(200);

            builder.Property(x => x.AuthorFullQualifyName).HasMaxLength(200);

            builder.Property(x => x.WriteTime).HasMaxLength(30);
        }
    }
}
