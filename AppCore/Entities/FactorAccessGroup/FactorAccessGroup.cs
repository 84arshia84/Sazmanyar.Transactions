using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;

namespace AppCore.Entities.FactorAccessGroup
{
    public class FactorAccessGroup
    {
        public Guid Id { get; set; }
        /// <summary>
        /// عنوان گروه دسترسی
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// توضیحات گروه دسترسی
        /// </summary>
        public string? Description { get; set; }



        public ICollection<FactorAccessGroupGroups> FactorAccessGroupGroupChildren { get; set; }
        public ICollection<FactorAccessGroupGroups> factorAccessGroupGroups { get; set; }
        public ICollection<FactorAccessGroupUsers> factorAccessGroupUsers  { get; set; }
        public ICollection<FactorAccessGroupRoleOfOrganizations> factorAccessGroupRoleOfOrganizations { get; set; }
        public ICollection<FactorAccessGroupFactorType> factorAccessGroupFactorTypes { get; set; }
        public ICollection<FactorAccessGroupOrganizationUnits> factorAccessGroupOrganizationUnits { get; set; }
        public Guid AccessGroupPropertiesId { get; set; }
        public FactorAccessGroupProperties factorAccessGroupProperties { get; set; }
        public Guid AccessGroupPermissionsId { get; set; }
        public FactorAccessGroupPermissions factorAccessGroupPermissions { get; set; }
    }
}
