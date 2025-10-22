using AppCore.Entities.SettingEntities.StagesRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class StagesRolesConfiguration : IEntityTypeConfiguration<StagesRoles>
    {
        public void Configure(EntityTypeBuilder<StagesRoles> builder)
        {
            builder.ToTable("StagesRoles", "TAM");
            builder.HasKey(sr => sr.Id);
            builder.HasOne(sr => sr.Role)
            .WithMany()
            .HasForeignKey(sr => sr.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
            builder.Property(sr => sr.StageId).IsRequired();
        }
    }
}
