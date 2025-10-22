using AppCore.Entities.ContractsInformation.ContractFinancialDetailes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractInformation
{
    internal interface IContractFinancialDetailsService
    {
        public Task<bool> AddFinancialDetaile(ContractFinancialDetails contractFinancial);
        public Task<bool> UpdateFinancialDetaile(ContractFinancialDetails contractFinancial);
        public Task<ContractFinancialDetails> GetFinancialDetails(Guid contractId);

    }
}
