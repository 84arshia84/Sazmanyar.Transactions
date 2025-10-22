using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.FactorInformation.FactorFinancialDetailes;
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.FactorInformation.FactorServiceExplanations;
using AppCore.Entities.FactorInformation.FactorTimeProfiles;
using AppCore.Entities.SettingEntities.AddendumTypes;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Entities.SettingEntities.FactorTypes;
using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using AppCore.Entities.SettingEntities.TransActionTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.FactorConfigs
{
    internal class FactorConfiguration : IEntityTypeConfiguration<Factor>
    {
        public void Configure(EntityTypeBuilder<Factor> builder)
        {
            builder.ToTable("Factors", "TAM");

            builder.HasKey(x => x.Id);

            builder.HasOne<FactorTimeProfile>(f => f.FactorTimeProfile)
            .WithOne(ft => ft.Factor)
            .HasForeignKey<Factor>(f => f.FactorTimeProfileId);

            builder.HasOne<FactorFinancialDetaile>(f => f.FactorFinancialDetaile)
            .WithOne(ft => ft.Factor)
            .HasForeignKey<Factor>(f => f.FactorFinancialDetaileId);

            builder.HasOne<FactorType>(f => f.FactorType)
            .WithMany(ft => ft.Factors)
            .HasForeignKey(f => f.FactorTypeId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Organizationalunit>(f => f.Organizationalunit)
            .WithMany(ft => ft.Factors)
            .HasForeignKey(f => f.OrganizationalunitId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<RoleOfOrganization>(f => f.RoleOfOrganization)
            .WithMany(ft => ft.Factors)
            .HasForeignKey(f => f.RoleOfOrganizationId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<CreditSource>(f => f.CreditSource)
            .WithMany(ft => ft.Factors)
            .HasForeignKey(f => f.CreditSourceID)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Property(f => f.CurrentStatusTitle).HasMaxLength(300);

            builder.Property(f=>f.LastActionTitle).HasMaxLength(300);

        }
    }
}
