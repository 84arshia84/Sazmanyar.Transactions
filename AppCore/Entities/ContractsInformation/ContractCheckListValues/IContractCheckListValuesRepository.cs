using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractCheckListValues
{
    public interface IContractCheckListValuesRepository
    {
        public Task<bool> Add(ContractCheckListValue contractCheckListValue);
        public Task<bool> Update(ContractCheckListValue contractCheckListValue);
        public Task<bool> Delete(Guid contractId);
        public Task<List<ContractCheckListValue>> GetAll();
        public Task<ContractCheckListValue> Get(Guid contractId);
    }
}
