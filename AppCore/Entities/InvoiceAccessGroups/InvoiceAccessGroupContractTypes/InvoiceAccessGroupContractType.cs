using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.RoleOfOrganizations;

namespace AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupContractTypes
{
    public class InvoiceAccessGroupContractType
    {
        public Guid Id { get; set; }
        public Guid ContractTypeId { get; set; }
        public Guid InvoiceAccessGroupId { get; set; }
        public InvoiceAccessGroup InvoiceAccessGroup { get; set; }
        public ContractType ContractType { get; set; }
    }
}
