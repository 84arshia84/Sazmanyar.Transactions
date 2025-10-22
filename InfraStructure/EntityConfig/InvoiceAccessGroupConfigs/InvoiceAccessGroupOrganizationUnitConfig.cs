using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupOrganizationUnits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.EntityConfig.InvoiceAccessGroupConfigs
{
    public class InvoiceAccessGroupOrganizationUnitConfig : IEntityTypeConfiguration<InvoiceAccessGroupOrganizationUnit>
    {
        public void Configure(EntityTypeBuilder<InvoiceAccessGroupOrganizationUnit> builder)
        {
            builder.ToTable("InvoiceAccessGroupOrganizationUnits", "TAM").HasKey(ct => ct.Id);
            builder.HasOne(a => a.InvoiceAccessGroup).WithMany(k => k.InvoiceAccessGroupOrganizationUnits)
                .HasForeignKey(a => a.InvoiceAccessGroupId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.OrganizationalUnit).WithMany(i => i.InvoiceAccessGroupOrganizationUnits)
                .HasForeignKey(o => o.OrganizationUnitId);
            builder.HasIndex(a => a.InvoiceAccessGroupId);
            builder.HasIndex(a => a.OrganizationUnitId);
        }
    }
}
