using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.AccessGroupGroups;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupContractTypes;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupInvoiceTypes;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupOrganizationUnits;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupRoleOfOrganizations;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupUsers;
using AppCore.Entities.SettingEntities.ContractTypes;

namespace AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups
{
    public class InvoiceAccessGroup
    {
        public Guid Id{ get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<InvoiceAccessGroupGroup> InvoiceAccessGroupGroupChildren { get; set; }
        public ICollection<InvoiceAccessGroupGroup> InvoiceAccessGroupGroups { get; set; }
        public ICollection<InvoiceAccessGroupUser> InvoiceAccessGroupUsers { get; set; }
        public ICollection<InvoiceAccessGroupProperties.InvoiceAccessGroupProperties> InvoiceAccessGroupProperties { get; set; }
        public ICollection<InvoiceAccessGroupInvoiceType> InvoiceAccessGroupInvoiceTypes { get; set; }
        public ICollection<InvoiceAccessGroupRoleOfOrganization> InvoiceAccessGroupRoleOfOrganizations { get; set; }
        public ICollection<InvoiceAccessGroupContractType> InvoiceAccessGroupContractTypes { get; set; }
        public ICollection<InvoiceAccessGroupOrganizationUnit> InvoiceAccessGroupOrganizationUnits { get; set; }
        public ICollection<InvoiceAccessGroupPermissions.InvoiceAccessGroupPermissions> InvoiceAccessGroupPermissions { get; set; }
    }
}
