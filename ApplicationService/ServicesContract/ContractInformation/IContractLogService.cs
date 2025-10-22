using AppCore.Entities.ContractsInformation.Contratcs;
using ApplicationService.DtoModels.ContractDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractInformation
{
    public interface IContractLogService
    {

        Task AddContractLog(Contract oldContract, Contract newContract, string updatedBy);
        Task<List<ContractLogDto>> GetLogsByContractId(Guid contractId);
    }
}
