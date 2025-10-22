using AppCore.Entities.SettingEntities.CorespondAndTypeOfCoopRel;
using AppCore.Entities.SettingEntities.CorespondentReals;
using AppCore.Entities.SettingEntities.TypeOfCooperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class CorespondRealTypeOfCoopRelConfiguration : IEntityTypeConfiguration<CorespondRealAndTypeOfCoopRel>
    {
        public void Configure(EntityTypeBuilder<CorespondRealAndTypeOfCoopRel> builder)
        {
            builder.HasKey(x => x.ID);

            builder
            .HasOne<CorespondentReal>()
            .WithMany(c => c.CorespondAndTypeOfCoopRels)
            .HasForeignKey(r => r.CorespondentRealID)
            .OnDelete(DeleteBehavior.NoAction);

            builder
            .HasOne<TypeOfCooperation>()
            .WithMany(c => c.CorespondRealAndTypeOfCoopRels)
            .HasForeignKey(r => r.TypeOfCooperationID)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
