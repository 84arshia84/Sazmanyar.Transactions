using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractInformation
{
    public interface IContractCheckListValuesService
    {
        public Task<(string message,bool isSuccess)> AddCheckListValues(ContractCheckListValue contractCheckListValue);
        public Task<(string message, bool isSuccess)> UpdateCheckListValues(ContractCheckListValue contractCheckListValue);
        public Task<(string message, bool isSuccess)> DeleteCheckListValues(Guid contractId);
        public Task<List<ContractCheckListValue>> GetAll();
        public Task<ContractCheckListValue> GetCheckListValues(Guid contractId);
    }
}
