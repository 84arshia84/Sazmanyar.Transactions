using AppCore.Entities.Organizations;
using AppCore.Entities.SettingEntities.TransActionTypes;
using InfraStructure.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.EntityConfig.AccountConfig
{
    internal class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasOne<OrganizationInformation>(c => c.Information)
                .WithMany(ta => ta.Accounts)
                .HasForeignKey(c => c.OrganizationInformationId);
     
        }
    }
}
