using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.OnAccountDepreciations
{
    public interface IOnAccountDepreciationRepository
    {
        public Task<bool> Add(OnAccountDepreciation onAccountDepreciation);
        public Task<bool> Update(OnAccountDepreciation onAccountDepreciation);
        public Task<OnAccountDepreciation> Get(Guid id);
        public Task<OnAccountDepreciation> GetByFinancialId(Guid id);
        public Task<List<OnAccountDepreciation>> GetAllByInvoiceId(Guid invoiceId);
        public Task<List<OnAccountDepreciation>> GetAllByFinancialId(Guid financialId);
        public Task<List<OnAccountDepreciation>> GetAllByServiceExplenationId(Guid explenationId);
        public Task<List<OnAccountDepreciation>> GetAllForContract(Guid contractId);
    }
}
