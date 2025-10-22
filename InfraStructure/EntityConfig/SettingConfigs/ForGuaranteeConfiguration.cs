using AppCore.Entities.SettingEntities.ForGuarantees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class ForGuaranteeConfiguration : IEntityTypeConfiguration<ForGuarantee>
    {
        public void Configure(EntityTypeBuilder<ForGuarantee> builder)
        {
            builder.HasMany(x => x.ContractGuarantees);
        }
    }
}
