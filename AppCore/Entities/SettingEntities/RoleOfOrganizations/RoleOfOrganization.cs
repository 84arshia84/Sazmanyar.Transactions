using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.FactorAccessGroup;
using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupRoleOfOrganizations;
using AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.RoleOfOrganizations
{
    /// <summary>
    /// نقش سازمان
    /// </summary>
    public class RoleOfOrganization :SettingEntity
    {
        public OrganizationRoleTypeEnum? OrganizationRoleType { get; set; }
        #region relation
        public List<Factor>? Factors { get; set; }
        public ICollection<InvoiceAccessGroupRoleOfOrganization> InvoiceAccessGroupRoleOfOrganizations { get; set; }
        public ICollection<ContractAccessGroupRoleOfOrganizations>? ContractAccessGroupRoleOfOrganizations { get; set; }
        public ICollection<FactorAccessGroupRoleOfOrganizations> FactorAccessGroupRoleOfOrganizations { get; set; } 
        #endregion
    }
}
