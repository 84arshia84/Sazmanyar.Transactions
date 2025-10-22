using AppCore.Entities.Attaches;
using AppCore.Entities.ContractsInformation.Contratcs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.AttachConfig
{
    internal class AttachConfiguration : IEntityTypeConfiguration<Attach>
    {
        public void Configure(EntityTypeBuilder<Attach> builder)
        {
            builder.ToTable("Attach", "TAM");

            builder.HasKey(x => x.Id);

            builder.Property(x=>x.UserUploader).HasMaxLength(128);

            builder.Property(x=>x.UserUploaderName).HasMaxLength(128);

            builder.Property(x=>x.FileExtention).HasMaxLength(10);

            builder.Property(x => x.FileName).HasMaxLength(300);

        }
    }
}
