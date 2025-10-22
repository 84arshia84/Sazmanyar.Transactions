using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractFinancialDetailes
{
    public interface IContractFinancialDetailsRepository
    {
        public Task<bool> Add(ContractFinancialDetails contratTimeProfile);
        public Task<ContractFinancialDetails> Get(Guid ContractId);
        public Task<bool> Update(ContractFinancialDetails contratTimeProfile);
    }
}
