using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractInformation
{
    public interface IContractAddendumService
    {
        public Task<(string message,bool isSuccess)> Add(ContractAddendumDto contractAddendum, string user);
        public Task<(string message, bool isSuccess)> Add(List<ContractAddendumDto> contractAddendum);
        public Task<(string message, bool isSuccess)> Update(ContractAddendumDto contractAddendum, LoginUserDto user);
        public Task<(string message, bool isSuccess)> Delete(Guid addendumId, string userName);
        public Task<ContractAddendumDto> Get(Guid addendumId);
        public Task<Guid> GetLastAddendumOfContract(Guid contractId);
        public Task<List<ContractAddendumDto>> GetAll(string username,int situation);
        public Task<List<ContractAddendumDto>> GetAll(Guid contractId);
        public Task<List<ContractAddendumDto>> GetAll(string username, Guid contractId);
    }
}
