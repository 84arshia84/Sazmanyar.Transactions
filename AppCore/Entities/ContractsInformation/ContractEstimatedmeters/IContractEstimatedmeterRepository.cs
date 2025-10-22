using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractEstimatedmeters
{
    public interface IContractEstimatedmeterRepository
    {
        public Task<bool> Add(ContractEstimatedmeter estimatedmeter);
        public Task<bool> Add(List<ContractEstimatedmeter> estimatedmeters);
        public Task<bool> Update(ContractEstimatedmeter estimatedmeter);
        public Task<bool> Update(List<ContractEstimatedmeter> estimatedmeter,Guid contractId);
        public Task<bool> UpdateInAddendum(ContractEstimatedmeter estimatedmeters);
        public Task<bool> Delete(Guid id);
        public Task<bool> DeleteInAddendum(ContractEstimatedmeter estimatedmeter);
        public Task<bool> DeleteAll(Guid contractId); 
        public Task<ContractEstimatedmeter> Get(Guid id);
        public Task<List<ContractEstimatedmeter>> GetAll(Guid ContractId);
        public Task<List<ContractEstimatedmeter>> GetAllForAddendum(Guid ContractId);

    }
}
