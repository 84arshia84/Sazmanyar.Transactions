using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupPermissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.EntityConfig.InvoiceAccessGroupConfigs
{
    public class InvoiceAccessGroupPermissionsConfig : IEntityTypeConfiguration<InvoiceAccessGroupPermissions>
    {
        public void Configure(EntityTypeBuilder<InvoiceAccessGroupPermissions> builder)
        {
            builder.ToTable("InvoiceAccessGroupPermissions", "TAM").HasKey(u => u.Id);
            builder.HasOne(a => a.InvoiceAccessGroup).WithMany(k => k.InvoiceAccessGroupPermissions).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(a => a.InvoiceAccessGroupId);
        }
    }
}
