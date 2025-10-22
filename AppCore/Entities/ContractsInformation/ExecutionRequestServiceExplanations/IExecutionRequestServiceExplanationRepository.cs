using AppCore.Entities.ContractsInformation.ServiceExplanations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations
{
    public interface IExecutionRequestServiceExplanationRepository
    {
        public Task<List<ExecutionRequestServiceExplanation>> GetAll(Guid transactionId);
        public Task<ExecutionRequestServiceExplanation> Get(Guid id);
        public Task<(string message, bool isSuccess)> Add(List<ExecutionRequestServiceExplanation> serviceExplanation);
        public Task<(string message, bool isSuccess)> Add(ExecutionRequestServiceExplanation serviceExplanation);
        public Task<(string message, bool isSuccess)> Delete(Guid id);
        public Task<(string message, bool isSuccess)> Update(ExecutionRequestServiceExplanation serviceExplanation);
        public  Task<List<ExecutionRequestServiceExplanation>> GetAllWithTransnactionApproved(Guid transactionId);
        // get For Contract
        public Task<List<ExecutionRequestServiceExplanation>> GetWithComodity(Guid transactionId);
    }
}
