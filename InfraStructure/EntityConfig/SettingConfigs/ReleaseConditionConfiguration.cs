using AppCore.Entities.SettingEntities.ReleaseConditions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class ReleaseConditionConfiguration : IEntityTypeConfiguration<ReleaseCondition>
    {
        public void Configure(EntityTypeBuilder<ReleaseCondition> builder)
        {
            builder.HasMany(x => x.ContractGuarantees);
        }
    }
}
