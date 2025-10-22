using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractAccessGroupConfig
{
    internal class ContractAccessGroupPropertiesConfig : IEntityTypeConfiguration<ContractAccessGroupProperties>
    {
        public void Configure(EntityTypeBuilder<ContractAccessGroupProperties> builder)
        {
            builder.ToTable("ContractAccessGroupProperties", "TAM").HasKey(u => u.Id);

            builder.HasOne<ContractAccessGroup>(c => c.ContractAccessGroup)
            .WithOne(ct => ct.ContractAccessGroupProperties)
            .HasForeignKey<ContractAccessGroupProperties>(c => c.ContractAccessGroupId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.ContractAccessGroupId);

            builder.Property(x => x.ContractTypeView).HasDefaultValue(0);

            builder.Property(x => x.ContractTypeEdit).HasDefaultValue(0);

            builder.Property(x => x.ContractTypeDelete).HasDefaultValue(0);

            builder.Property(x => x.RoleOfOrganizationView).HasDefaultValue(0);

            builder.Property(x => x.RoleOfOrganizationEdit).HasDefaultValue(0);

            builder.Property(x => x.RoleOfOrganizationDelete).HasDefaultValue(0);

            builder.Property(x => x.OrganizationUnitView).HasDefaultValue(0);

            builder.Property(x => x.OrganizationUnitEdit).HasDefaultValue(0);

            builder.Property(x => x.OrganizationUnitDelete).HasDefaultValue(0);


        }
    }
}
