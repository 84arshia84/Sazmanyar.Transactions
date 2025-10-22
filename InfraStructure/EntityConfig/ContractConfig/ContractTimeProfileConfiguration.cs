using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.Entities.SettingEntities.BasisForStartingTheProjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.ContractConfig
{
    internal class ContractTimeProfileConfiguration : IEntityTypeConfiguration<ContractTimeProfile>
    {
        public void Configure(EntityTypeBuilder<ContractTimeProfile> builder)
        {
            builder.HasKey(ct => ct.ID);

            builder.HasOne<BasisForStartingTheProject>(ct => ct.BasisForStartingTheProject)
            .WithMany(bf => bf.ContractTimeProfile)
            .HasForeignKey(ct => ct.BasisForStartingProjectID);
        }
    }
}
