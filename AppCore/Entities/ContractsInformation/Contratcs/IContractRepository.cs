using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractsInformation.Contratcs
{
    public interface IContractRepository
    {
        public Task<(string message, bool isSuccess)> Add(Contract contract);
        public Task<List<Contract>> GetAll();
        public Task<List<Contract>> GetAllWithFinalApprove();
        public Task<List<Contract>> GetAllMine(Guid userId);
        public Task<List<Contract>> GetAllWaitForAction(List<Guid> contractId);
        public Task<List<Contract>> GetAllForAddendum(List<Guid> contractId);
        public Task<List<Contract>> GetAllForInvoice(List<Guid> contractId);
        public Task<Contract> Get(Guid contractId);
        public Task<Contract> HasInvoice(Guid contractId);
        public Task<(string message, bool isSuccess)> Delete(Guid contractId, Guid userId);
        public Task DeleteExceptinalContract(Guid contractId);
        public Task<(string message, bool isSuccess)> Update(Contract contract);
        public Task<bool> UpdateStatus(Guid contractId, Guid newId);
        public Task<List<Contract>> SearchAllAsync(string? term);
        public Task<List<Contract>> SearchMineAsync(Guid userId, string? term);
        public Task<List<Contract>> SearchWaitForActionAsync(List<Guid> contractIds, string? term);
        public Task<List<Contract>> SearchWithFinalApproveAsync(string? term);
        public Task<List<Contract>> GetWithIsFromExecution();
        // کد قرارداد غیر تکراری
        public Task<Contract?> GetByContractNumber (string contractNumber);

    }
}
