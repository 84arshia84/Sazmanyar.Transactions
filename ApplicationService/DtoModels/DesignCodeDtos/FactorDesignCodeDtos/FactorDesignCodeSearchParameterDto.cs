using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.DesignCodeDtos.FactorDesignCodeDtos
{
    public class FactorDesignCodeSearchParameterDto
    {
        public Guid FactorTypeId { get; set; }
        public string FactorType { get; set; }
        public Guid RoleOfOrganizationId { get; set; }
        public string RoleOfOrganization { get; set; }
        public Guid OrganizationUnitId { get; set; }
        public string OrganizationUnit { get; set; }
    }
}
