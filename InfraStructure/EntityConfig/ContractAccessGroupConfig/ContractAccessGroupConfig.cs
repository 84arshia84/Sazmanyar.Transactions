using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractAccessGroupConfig
{
    internal class ContractAccessGroupConfig : IEntityTypeConfiguration<ContractAccessGroup>
    {
        public void Configure(EntityTypeBuilder<ContractAccessGroup> builder)
        {
            builder.ToTable("ContractAccessGroups", "TAM").HasKey(x => x.Id);

            builder.Property(c=>c.Title).HasMaxLength(350);

            builder.Ignore(x => x.AccessGroupPermissionsId);

            builder.Ignore(x => x.AccessGroupPropertiesId);
        }
    }
}
