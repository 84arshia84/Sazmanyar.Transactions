using AppCore.Entities.ContractsInformation.ContractGuarantees;
using ApplicationService.DtoModels.ContractDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractInformation
{
    public interface IContractGuaranteeService
    {
        public Task<bool> AddGuarantee(List<ContractGuarantee> contractGuarantee);
        public Task<List<ContractGuaranteeDto>> GetAll(Guid contractId);
        public Task<bool> DeleteGuarantee (Guid contractGuarantee);
        public Task<bool> UpdateGuarantee (List<ContractGuarantee> contractGuarantee);
    }
}
