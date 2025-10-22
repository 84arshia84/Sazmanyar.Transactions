using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using AppCore.Entities.SettingEntities.Organizationalunits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractAccessGroups
{
    /// <summary>
    /// رابط واحد سازمانی و گروه دسترسی معاملات
    /// </summary>
    public class ContractAccessGroupOrganizationUnits
    {
        #region properties
        public Guid Id { get; set; }
        public Guid OrganizationUnitId { get; set; }
        public Guid ContractAccessGroupId { get; set; }
        #endregion
        #region relations
        public ContractAccessGroup ContractAccessGroup { get; set; }
        public Organizationalunit OrganizationalUnit { get; set; }
        #endregion
    }
}
