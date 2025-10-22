using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class RoleOfOrganizationConfiguration : IEntityTypeConfiguration<RoleOfOrganization>
    {
        public void Configure(EntityTypeBuilder<RoleOfOrganization> builder)
        {
            builder.ToTable("RoleOfOrganizations", "TAM");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.Title).HasMaxLength(200);
        }
    }
}
