using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.DesignCodeDtos.TransactionExecutionDesignCodeDtos
{
    public class TransactionDesignCodeSearchParameterDto
    {
        public Guid ContractTypeIds { get; set; }
        public string ContractType { get; set; }
        public Guid OrganizationUnitId { get; set; }
        public string OrganizationUnit { get; set; }
    }
}
