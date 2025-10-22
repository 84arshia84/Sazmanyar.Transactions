using AppCore.Entities.SettingEntities.CheckLists;
using AppCore.Entities.SettingEntities.Statuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class StatusConfigiuration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Status", "TAM");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.Title).HasMaxLength(200);

        }
    }
}
