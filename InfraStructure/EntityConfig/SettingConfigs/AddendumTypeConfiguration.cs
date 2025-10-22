using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.SettingEntities.AddendumTypes;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class AddendumTypeConfiguration : IEntityTypeConfiguration<AddendumType>
    {
        public void Configure(EntityTypeBuilder<AddendumType> builder)
        {
            builder.ToTable("AddendumTypes", "TAM");

            builder.HasKey(x => x.ID);

            builder.HasMany(x => x.ContractAddendum);

           
        }
    }
}
