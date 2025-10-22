using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;

namespace AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupRoleOfOrganizations
{
    public class InvoiceAccessGroupRoleOfOrganization
    {
        public Guid Id { get; set; }
        public Guid RoleOfOrganizationId { get; set; }
        public Guid InvoiceAccessGroupId { get; set; }
        public InvoiceAccessGroup InvoiceAccessGroup { get; set; }
        public RoleOfOrganization RoleOfOrganization { get; set; }
    }
}
