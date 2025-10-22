using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.DesignCodeDtos.InvoiceDesignCodeDtos
{
    public class InvoiceDesignCodeGetParameterDto
    {
        public Guid ContractTypeIds { get; set; }
        public Guid RoleOfOrganizationId { get; set; }
        public Guid OrganizationUnitId { get; set; }
        public Guid InvoiceTypeId { get; set; }
    }
}
