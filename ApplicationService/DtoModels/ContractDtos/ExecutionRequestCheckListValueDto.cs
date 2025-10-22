using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ContractDtos
{
    public class ExecutionRequestCheckListValueDto
    {
        public Guid id { get; set; }
        public string? checkListValues { get; set; }
    }
}
