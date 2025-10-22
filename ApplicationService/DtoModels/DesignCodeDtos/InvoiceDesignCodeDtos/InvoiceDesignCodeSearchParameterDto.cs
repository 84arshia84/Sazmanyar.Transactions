using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.DesignCodeDtos.InvoiceDesignCodeDtos
{
    public class InvoiceDesignCodeSearchParameterDto
    {
        public Guid ContractTypeIds { get; set; }
        public string ContractType { get; set; }
        public Guid RoleOfOrganizationId { get; set; }
        public string RoleOfOrganization { get; set; }
        public Guid OrganizationUnitId { get; set; }
        public string OrganizationUnit { get; set; }
        public Guid InvoiceTypeId { get; set; }
        public string InvoiceType { get; set; }
    }
}
