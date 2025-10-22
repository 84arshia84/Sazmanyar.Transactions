using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupRoleOfOrganizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.EntityConfig.InvoiceAccessGroupConfigs
{
    public class InvoiceAccessGroupRoleOfOrganizationConfig : IEntityTypeConfiguration<InvoiceAccessGroupRoleOfOrganization>
    {
        public void Configure(EntityTypeBuilder<InvoiceAccessGroupRoleOfOrganization> builder)
        {
            builder.ToTable("InvoiceAccessGroupRoleOfOrganizations", "TAM").HasKey(ct => ct.Id);
            builder.HasOne(a => a.InvoiceAccessGroup).WithMany(k => k.InvoiceAccessGroupRoleOfOrganizations).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(a => a.InvoiceAccessGroupId);
            builder.HasIndex(a => a.RoleOfOrganizationId);
        }
    }
}
