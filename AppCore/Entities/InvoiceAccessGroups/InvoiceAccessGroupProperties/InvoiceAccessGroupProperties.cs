using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;

namespace AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupProperties
{
    public class InvoiceAccessGroupProperties
    {
        public Guid Id { get; set; }
        public Guid InvoiceAccessGroupId { get; set; }
        public bool InvoiceTypeSave { get; set; }
        public bool InvoiceTypeView { get; set; }
        public bool InvoiceTypeEdit { get; set; }
        public bool InvoiceTypeDelete { get; set; }
        public bool ContractTypeSave { get; set; }
        public bool ContractTypeView { get; set; }
        public bool ContractTypeEdit { get; set; }
        public bool ContractTypeDelete { get; set; }
        public bool RoleOfOrganizationSave { get; set; }
        public bool RoleOfOrganizationView { get; set; }
        public bool RoleOfOrganizationEdit { get; set; }
        public bool RoleOfOrganizationDelete { get; set; }
        public bool OrganizationUnitSave { get; set; }
        public bool OrganizationUnitView { get; set; }
        public bool OrganizationUnitEdit { get; set; }
        public bool OrganizationUnitDelete { get; set; }
        public InvoiceAccessGroup InvoiceAccessGroup { get; set; }
    }
}
