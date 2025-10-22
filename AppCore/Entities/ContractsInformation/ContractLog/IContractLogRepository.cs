using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContractLog
{
    public interface IContractLogRepository
    {
        Task Add(ContractLog log);
        Task<List<ContractLog>> GetByContractId(Guid contractId);
    }
}
