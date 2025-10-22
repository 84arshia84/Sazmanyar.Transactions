using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.DesignCodeDtos.ContractDesignCodeDtos
{
    public class ContractDesignCodeGetDto
    {
        public Guid Id { get; set; }
        public string Preview { get; set; }
        public string ParameterPreview { get; set; }
        public int Counter { get; set; }
        public List<Guid>? ContractTypeIds { get; set; }
        public List<Guid>? RoleOfOrganizationId { get; set; }
        public List<Guid>? OrganizationUnitId { get; set; }
    }
}
