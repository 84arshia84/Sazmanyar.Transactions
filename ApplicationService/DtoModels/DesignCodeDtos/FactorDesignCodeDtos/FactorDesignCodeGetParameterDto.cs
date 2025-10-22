using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.DesignCodeDtos.FactorDesignCodeDtos
{
    public class FactorDesignCodeGetParameterDto
    {
        public Guid? FactorTypeId { get; set; }
        public Guid? RoleOfOrganizationId { get; set; }
        public Guid? OrganizationUnitId { get; set; }
    }
}
