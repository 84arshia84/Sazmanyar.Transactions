using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using AppCore.Entities.ContractsInformation.ExecutionRequestCheckListValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractInformation
{
    public interface IExecutionRequestCheckListValueService
    {
        public Task<(string message, bool isSuccess)> AddCheckListValues(ExecutionRequestCheckListValue CheckListValue);
        public Task<(string message, bool isSuccess)> UpdateCheckListValues(ExecutionRequestCheckListValue CheckListValue);
        public Task<(string message, bool isSuccess)> DeleteCheckListValues(Guid transactionId);
        public Task<ExecutionRequestCheckListValue> GetCheckListValues(Guid transactionId);
    }
}
