using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.DesignCodeDtos.TransactionExecutionDesignCodeDtos
{
    public class TransactionDesignCodeGetParameterDto
    {
        public Guid? ContractTypeIds { get; set; }
        public Guid? OrganizationUnitId { get; set; }
    }
}
