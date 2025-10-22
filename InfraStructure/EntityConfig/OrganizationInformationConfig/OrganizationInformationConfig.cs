using AppCore.Entities.ContractsInformation.ContractCoefficients;
using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.Entities.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.OrganizationInformationConfig
{
    public class OrganizationInformationConfig : IEntityTypeConfiguration<OrganizationInformation>
    {
        public void Configure(EntityTypeBuilder<OrganizationInformation> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasMany<Account>(c => c.Accounts)
            .WithOne(cc => cc.Information)
            .HasForeignKey(cc => cc.OrganizationInformationId);
        }
    }
}
