using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.ContratTimeProfiles
{
    public interface IContractTimeProfileRepository
    {
        public Task<bool> Add(ContractTimeProfile contratTimeProfile);
        public Task<ContractTimeProfile> Get(Guid ContractId);
        public Task<bool> Update(ContractTimeProfile contratTimeProfile);
    }
}
