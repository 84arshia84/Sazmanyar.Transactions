using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.EntityConfig.InvoiceAccessGroupConfigs
{
    public class InvoiceAccessGroupUserConfig : IEntityTypeConfiguration<InvoiceAccessGroupUser>
    {
        public void Configure(EntityTypeBuilder<InvoiceAccessGroupUser> builder)
        {
            builder.ToTable("InvoiceAccessGroupUsers", "TAM").HasKey(u => u.Id);
            builder.HasOne(a => a.AccessGroup).WithMany(k => k.InvoiceAccessGroupUsers).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(a => a.AccessGroupId);
        }
    }
}
