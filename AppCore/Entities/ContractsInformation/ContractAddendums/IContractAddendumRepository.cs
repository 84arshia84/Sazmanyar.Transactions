using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractAddendums
{
    public interface IContractAddendumRepository
    {
        public Task<bool> Add(ContractAddendum contractAddendum);
        public Task<bool> Add (List<ContractAddendum> contractAddendum);
        public Task<bool> Update(ContractAddendum contractAddendum);
        public Task UpdateAddendumChangedValue(Guid addendumId, string value);
        public Task<bool> Delete(Guid addendumId,Guid userId);
        public Task<bool> DeleteExceptionalAddendum(Guid addendumId);
        public Task<ContractAddendum> Get(Guid addendumId);
        public Task<ContractAddendum> GetByContract(Guid addendumId);
        public Task<Guid> GetLastAddendumOfContract(Guid contractId);
        public Task<List<ContractAddendum>> GetAll();
        public Task<List<ContractAddendum>> GetAll(List<Guid> contractId);
        public Task<List<ContractAddendum>> GetAll(Guid contractId);
        public Task<List<ContractAddendum>> GetAllMine(Guid userId);
        public Task<List<ContractAddendum>> GetAllWaitForAction(List<Guid> contractId);
    }
}
