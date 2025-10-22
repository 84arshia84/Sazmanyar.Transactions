using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractInformation
{
    internal interface IContractTimeProfileService
    {
        public Task<bool> AddProfile(ContractTimeProfile contractTime);
        public Task<bool> UpdateProfile(ContractTimeProfile contractTime);
        public Task<ContractTimeProfile> GetProfile(Guid contractId);
    }
}
