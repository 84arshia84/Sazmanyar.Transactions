using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.DesignCodeDtos.ContractDesignCodeDtos
{
    public class ContractDesignCodeSearchParameterDto
    {
        public Guid ContractTypeIds { get; set; }
        public string ContractType { get; set; }
        public Guid RoleOfOrganizationId { get; set; }
        public string RoleOfOrganization { get; set; }
        public Guid OrganizationUnitId { get; set; }
        public string OrganizationUnit { get; set; }
    }
}
