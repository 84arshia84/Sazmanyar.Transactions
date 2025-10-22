using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.ContractInformation
{
    public interface ITransactionExecutionRequestService
    {
        public Task<(string message, bool isSuccess)> Add(TransactionExecutionRequestDto transactionDto, LoginUserDto userDto);
        public Task<(string message, bool isSuccess)> Delete(Guid id, string userDto);
        public Task<(string message, bool isSuccess)> Update(TransactionExecutionRequestDto transactionDto, LoginUserDto userDto);
        public Task<TransactionExecutionRequestDto> Get(Guid id);
        public Task<List<TransactionExecutionRequestDto>> GetAll(string userName, int situation);
        public Task<List<TransactionExecutionRequestSubjectAndIdDto>> GetAllApproved(string userName);


    }
}
