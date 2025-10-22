using AppCore.Entities.Organizations;
using AppCore.Entities.SettingEntities.CheckLists;
using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.LookUpTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.SettingConfigs
{
    internal class CheckListConfiguration : IEntityTypeConfiguration<CheckList>
    {
        public void Configure(EntityTypeBuilder<CheckList> builder)
        {
            builder.ToTable("CheckLists", "TAM");

            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Title).HasMaxLength(200);

            builder.Property(x=>x.Type).HasMaxLength(100);

            builder.HasOne<ContractType>(ch => ch.ContractType)
           .WithMany(ct => ct.CheckLists)
           .HasForeignKey(ch => ch.ContractTypeId);

            builder.HasOne<LookUpTable>(ch => ch.LookUpTable)
           .WithMany(ct => ct.CheckLists)
           .HasForeignKey(ch => ch.LookUpTableId);
            
            //builder.Property(c => c.SystemParts)
            //    .HasConversion<string>()
            //    .HasMaxLength(50);



        }
    }
}
