using AppCore.Entities.InvoiceAccessGroups.AccessGroupGroups;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupContractTypes;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupInvoiceTypes;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupOrganizationUnits;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupPermissions;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupProperties;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupRoleOfOrganizations;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractAccessGroups
{
    /// <summary>
    /// هسته اصلی گروه دسترسی معاملات
    /// </summary>
    public class ContractAccessGroup
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// عنوان گروه دسترسی
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// توضیحات گروه دسترسی
        /// </summary>
        public string? Description { get; set; }
        #endregion
        #region relations

        public ICollection<ContractAccessGroupGroups> ContractAccessGroupGroupChildren { get; set; }
        public ICollection<ContractAccessGroupGroups> ContractAccessGroupGroups { get; set; }
        public ICollection<ContractAccessGroupUsers> ContractAccessGroupUsers { get; set; }
        public ICollection<ContractAccessGroupRoleOfOrganizations> ContractAccessGroupRoleOfOrganizations { get; set; }
        public ICollection<ContractAccessGroupContractType> ContractAccessGroupContractTypes { get; set; }
        public ICollection<ContractAccessGroupOrganizationUnits> ContractAccessGroupOrganizationUnits { get; set; }
        public Guid AccessGroupPropertiesId { get; set; }
        public ContractAccessGroupProperties ContractAccessGroupProperties { get; set; }
        public Guid AccessGroupPermissionsId { get; set; }
        public ContractAccessGroupPermissions ContractAccessGroupPermissions { get; set; }
        public List<ContractAccessGroupSystemParts> ContractAccessGroupSystemParts { get; set; }
        #endregion
    }
}
