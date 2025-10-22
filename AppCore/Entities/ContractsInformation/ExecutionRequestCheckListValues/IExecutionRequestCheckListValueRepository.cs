using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ExecutionRequestCheckListValues
{
    public interface IExecutionRequestCheckListValueRepository
    {
        public Task<bool> Add(ExecutionRequestCheckListValue CheckListValue);
        public Task<bool> Update(ExecutionRequestCheckListValue CheckListValue);
        public Task<bool> Delete(Guid transactionId);
        public Task<ExecutionRequestCheckListValue> Get(Guid transactionId);
    }
}
