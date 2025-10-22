using AppCore.Entities.SettingEntities.RoleOfOrganizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractAccessGroups
{
    /// <summary>
    /// رابط نقش سازمان و گروه دسترسی معاملات
    /// </summary>
    public class ContractAccessGroupRoleOfOrganizations
    {
        #region properties
        public Guid Id { get; set; }
        public Guid RoleOfOrganizationId { get; set; }


        #endregion
        #region relations
        public Guid ContractAccessGroupId { get; set; }
        public ContractAccessGroup ContractAccessGroup { get; set; }
        public RoleOfOrganization RoleOfOrganization { get; set; }
        #endregion
    }
}
