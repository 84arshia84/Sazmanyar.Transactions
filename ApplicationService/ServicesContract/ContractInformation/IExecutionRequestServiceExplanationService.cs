using AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using ApplicationService.DtoModels.ContractDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractInformation
{
    public interface IExecutionRequestServiceExplanationService
    {
        public Task<(string message, bool isSuccess)> AddServiceExplanation(List<ExecutionRequestServiceExplanation> serviceExplanation);
        public Task<List<ExecutionRequestServiceExplanationDto>> GetAllServiceExplanations(Guid transactionId);
        public Task<(string message, bool isSuccess)> UpdateServiceExplanation(List<ExecutionRequestServiceExplanation> serviceExplanation);
        public Task<(string message, bool isSuccess)> DeleteServiceExplanation(Guid serviceExplenationId);
        public Task<List<ExecutionRequestServiceExplanationDto>> GetAllWithTransactionIsFinalApproved(Guid transactionId);
        public Task<List<ExecutionRequestServiceExplanationDto>> GetAllFromContract(Guid transactionId);
    }
}
