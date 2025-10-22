using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.DesignCodeDtos.TransactionExecutionDesignCodeDtos
{
    public class TransactionDesignCodeUpdateDto
    {
        public Guid Id { get; set; }
        public string DesignCode { get; set; }
        public string Preview { get; set; }
        public string ParameterPreview { get; set; }
        public List<Guid>? ContractTypeIds { get; set; }
        public List<Guid>? OrganizationUnitId { get; set; }
    }
}
