using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroupPermissions;

namespace ApplicationService.DtoModels.InvoiceDtos.InvoiceAccessGroupsDtos
{
    public class GetInvoiceAccessGroupDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Guid> InvoiceAccessGroupInvoiceTypes { get; set; }
        public List<Guid> InvoiceAccessGroupContractTypes { get; set; }
        public List<Guid> InvoiceAccessGroupRoleOfOrganizations { get; set; }
        public List<Guid> InvoiceAccessGroupOrganizationUnits { get; set; }
        public List<Guid>? InvoiceAccessGroupUsers { get; set; }
        public List<Guid>? InvoiceAccessGroupGroups { get; set; }
        public InvoiceAccessGroupPropertiesDto InvoiceAccessGroupProperties { get; set; }
        public InvoiceAccessGroupPermissionsDto InvoiceAccessGroupPermissions { get; set; }
    }
}