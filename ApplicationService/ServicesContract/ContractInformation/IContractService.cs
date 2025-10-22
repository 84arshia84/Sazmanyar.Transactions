using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractInformation
{
    public interface IContractService
    {
        #region Crud
        public Task<(string message, bool isSuccess)> Add(ContractDto contractDto, LoginUserDto userDto);
        public Task<(string message, bool isSuccess)> Delete(Guid id, string userDto);
        public Task<(string message, bool isSuccess)> Update(ContractDto contractDto, LoginUserDto userDto);
        public Task<bool> UpdateStatus(Guid contractId, Guid newId);
        public Task<(string message, bool isSuccess)> UpdateInAddendum(ContractDto contractDto, LoginUserDto userDto, Guid AddendumId);
        public Task<ContractDto> Get(Guid id);
        public Task<List<ContractDto>> GetAll(string userName, int situation);
        public Task<List<ContractDto>> GetAllForAddendum(string userName);
        public Task<List<ContractDto>> GetAllForInvoice(string userName);
        public Task<bool> HasInvoice(Guid contractId);
        public Task<string> GetLastContractAmount(Guid contractId);
        public Task<(string message, bool isSuccess)> AddByTranaction(ContractFromTransactionDto contractDto, LoginUserDto userDto);
        public Task<List<ContractDto>> GetAllWithIsForTransaction();

        public Task<List<ContractDto>> SearchForAddendum(string userName, string? term);
        public Task<List<ContractDto>> SearchForInvoice(string userName, string? term);

        #endregion
    }
}
