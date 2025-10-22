using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ContractDtos
{
    public class TransactionExecutionRequestSubjectAndIdDto
    {
        public Guid Id { get; set; }
        public string SubjectOfRequest { get; set; }
    }
}
