using AppCore.Entities.SettingEntities.TypeOfGuarantees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class TypeOfGuaranteeConfiguration : IEntityTypeConfiguration<TypeOfGuarantee>
    {
        public void Configure(EntityTypeBuilder<TypeOfGuarantee> builder)
        {
            builder.ToTable("TypeOfGuarantees", "TAM");

            builder.HasKey(x => x.ID);

            builder.HasMany(x => x.ContractGuarantees);
        }
    }
}
