using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.FactorTypes;

namespace AppCore.Entities.FactorAccessGroup
{
    public class FactorAccessGroupFactorType
    {
        public Guid Id { get; set; }
        public Guid FactorTypeId { get; set; }
        public Guid FactorAccessGroupId { get; set; }

        public FactorAccessGroup FactorAccessGroup { get; set; }
        public FactorType FactorType { get; set; }
    }
}
