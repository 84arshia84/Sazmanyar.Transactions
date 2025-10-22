using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorInformation.Factors
{
    public interface IFactorRepository
    {
        public Task<bool> Add(Factor factor);
        public Task<List<Factor>> GetAll(List<Guid> factorIds);
        public Task<List<Factor>> GetAllWithFinalApprove();
        public Task<List<Factor>> GetAllMine(Guid userId);
        public Task<List<Factor>> GetAllWaitForAction(List<Guid> factorId);
        public Task<Factor> Get(Guid factorId);
        public Task<bool> Delete(Guid factorId, Guid userId);
        public Task DeleteExceptinalFactor(Guid factorId);
        public Task<bool> Update(Factor factor);
        public Task<List<Factor>> SearchAllAsync(string? term);
        public Task<List<Factor>> SearchMineAsync(Guid userId, string? term);
        public Task<List<Factor>> SearchWaitForActionAsync(List<Guid> factorIds, string? term);
        public Task<List<Factor>> SearchWithFinalApproveAsync(string? term);
        // بررسی کد فاکتور
        public Task<Factor?> GetByFactorNumber(string? factorNumber);
    }
}
