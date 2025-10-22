using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;

namespace AppCore.Entities.FactorAccessGroup
{
    public class FactorAccessGroupRoleOfOrganizations
    {
        public Guid Id { get; set; }
        public Guid RoleOfOrganizationId { get; set; }
        public Guid FactorAccessGroupId { get; set; }
        public FactorAccessGroup FactorAccessGroup { get; set; }
        public RoleOfOrganization RoleOfOrganization { get; set; }
    }
}
