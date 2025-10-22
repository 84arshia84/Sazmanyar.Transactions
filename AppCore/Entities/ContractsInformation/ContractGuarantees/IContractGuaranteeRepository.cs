using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractGuarantees
{
    public interface IContractGuaranteeRepository
    {
        public Task<bool> Add(List<ContractGuarantee> contractGuarantee);
        public Task<bool> Add(ContractGuarantee contractGuarantee);
        public Task<ContractGuarantee> Get(Guid id);
        public Task<List<ContractGuarantee>> GetAll(Guid contractId);
        public Task<bool> Delete(Guid contractGuarantee);
        public Task<bool> Update(ContractGuarantee contractGuarantee);
    }
}
