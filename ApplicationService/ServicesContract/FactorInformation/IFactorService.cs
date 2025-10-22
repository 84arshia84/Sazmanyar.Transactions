using ApplicationService.DtoModels.FactorDtos.Factor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.FactorInformation
{
    public interface IFactorService
    {
        public Task<(string message, bool isSuccess)> Add(FactorAddDto factor, string userName);
        public Task<List<FactorGetDto>> GetAll(string userName);
        public Task<List<FactorGetDto>> GetAllWithFinalApprove();
        public Task<List<FactorGetDto>> GetAllMine(string userName);
        public Task<List<FactorGetDto>> GetAllWaitForAction(string userName);
        public Task<FactorGetDto> Get(Guid factorId);
        public Task<(string message, bool isSuccess)> Delete(Guid factorId, string userName);
        public Task<(string message, bool isSuccess)> Update(FactorUpdateDto factor, string username);
        public Task<List<FactorGetDto>> Search(string userName, int situation, string? term);
        public Task<List<FactorGetDto>> GetAllWaitForApprove(string userName);

    }
}
