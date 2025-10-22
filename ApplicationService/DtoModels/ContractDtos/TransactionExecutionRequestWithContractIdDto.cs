using ApplicationService.DtoModels.PublicEntitiesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ContractDtos
{
    public class TransactionExecutionRequestWithContractIdDto
    {
        public Guid id { get; set; }
        public Guid contractId { get; set; }
        public string subjectOfRequest { get; set; }
        public DateTime dateOfRequest { get; set; }
        public string numberOfRequest { get; set; }
        public string estimatedTimeForDoingRequest { get; set; }
        public string? explenationRequest { get; set; }
        public string? explenationRequestOfConsultant { get; set; }
        public Guid contractTypeId { get; set; }
        public Guid organizationId { get; set; }
        public DateTime? insertDate { get; set; }
        public bool IsForExecutionRequest { get; set; }
        public ExecutionRequestCheckListValueDto? checkListValueDto { get; set; }
        public List<ExecutionRequestServiceExplanationDto> serviceExplanationDtos { get; set; }
        public List<DescriptionDto>? descriptionDtos { get; set; }
        public List<AttachDto>? attachDtos { get; set; }
        public int row { get; set; }
        public bool? IsfinalApproved { get; set; }
    }
}
