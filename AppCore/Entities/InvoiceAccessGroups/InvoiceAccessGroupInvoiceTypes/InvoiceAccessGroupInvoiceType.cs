using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using AppCore.Entities.InvoicesInformations.InvoiceType;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;

namespace AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupInvoiceTypes
{
    public class InvoiceAccessGroupInvoiceType
    {
        public Guid Id { get; set; }
        public Guid InvoiceTypeId { get; set; }
        public Guid InvoiceAccessGroupId { get; set; }
        public InvoiceAccessGroup InvoiceAccessGroup { get; set; }
        public InvoiceType InvoiceType { get; set; }
    }
}
