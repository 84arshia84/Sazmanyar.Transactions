using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.SettingEntities.Organizationalunits;

namespace AppCore.Entities.FactorAccessGroup
{
    public class FactorAccessGroupOrganizationUnits
    {
        public Guid Id { get; set; }
        public Guid OrganizationUnitId { get; set; }
        public Guid FactorAccessGroupId { get; set; }

        public FactorAccessGroup FactorAccessGroup { get; set; }
        public Organizationalunit OrganizationalUnit { get; set; }
    }
}
