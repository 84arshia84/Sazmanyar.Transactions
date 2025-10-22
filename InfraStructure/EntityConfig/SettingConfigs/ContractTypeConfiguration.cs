using AppCore.Entities.SettingEntities.ContractTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class ContractTypeConfiguration : IEntityTypeConfiguration<ContractType>
    {
        public void Configure(EntityTypeBuilder<ContractType> builder)
        {
            builder.ToTable("ContractTypes", "TAM");

            builder.HasKey(x => x.ID);

            builder.HasMany(x => x.Contracts);

            builder.HasMany(x => x.CheckLists);
        }
    }
}
