using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.TransactionExecutionRequests
{
    public interface ITransactionExecutionRequestRepository
    {
        public Task<(string message, bool isSuccess)> Add(TransactionExecutionRequest transaction);
        public Task<List<TransactionExecutionRequest>> GetAll();
        public Task<List<TransactionExecutionRequest>> GetAllMine(Guid userId);
        public Task<List<TransactionExecutionRequest>> GetAllWaitForAction(List<Guid> transactionId);
        public Task<TransactionExecutionRequest> Get(Guid transactionId);
        public Task<(string message, bool isSuccess)> Delete(Guid transactionId, Guid userId);
        public Task DeleteExceptionalTransaction(Guid transactionId);
        public Task<(string message, bool isSuccess)> Update(TransactionExecutionRequest transaction);
        public  Task<List<TransactionExecutionRequest>> GetAllFinalApproved();
        // بررسی کد معامله 
        public Task<TransactionExecutionRequest?> GetByRequestNumber(string numberOfRequest);
    }
}
