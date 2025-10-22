using AppCore.Entities.SettingEntities.CorespondAndTypeOfCoopRel;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using AppCore.Entities.SettingEntities.TypeOfCooperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class CorespondTypeOfCoopRelConfiguration : IEntityTypeConfiguration<CoresponedAndTypeCoopRels>
    {
        public void Configure(EntityTypeBuilder<CoresponedAndTypeCoopRels> builder)
        {

            builder.HasKey(x => x.ID);

            builder
           .HasOne<CorespondentLegal>()
           .WithMany(c => c.CorespondAndTypeOfCoopRels)
           .HasForeignKey(r => r.CoresponedLegalID)
           .OnDelete(DeleteBehavior.NoAction);

            builder
            .HasOne<TypeOfCooperation>() 
            .WithMany(c => c.CorespondAndTypeOfCoopRels) 
            .HasForeignKey(r => r.TypeOfCoopreationID)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
