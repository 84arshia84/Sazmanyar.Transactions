using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.AccessGroupGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.EntityConfig.InvoiceAccessGroupConfigs
{
    public class InvoiceAccessGroupGroupConfig : IEntityTypeConfiguration<InvoiceAccessGroupGroup>
    {
        public void Configure(EntityTypeBuilder<InvoiceAccessGroupGroup> builder)
        {
            builder.ToTable("InvoiceAccessGroupGroups", "TAM").HasKey(u => u.Id);
            builder.HasOne(a => a.AccessGroup).WithMany(k => k.InvoiceAccessGroupGroups).HasForeignKey(a => a.GroupId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(a => a.AccessGroupParent).WithMany(k => k.InvoiceAccessGroupGroupChildren).HasForeignKey(a => a.ParentGroupId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(a => a.ParentGroupId);
            builder.HasIndex(a => a.GroupId);
        }
    }
}
