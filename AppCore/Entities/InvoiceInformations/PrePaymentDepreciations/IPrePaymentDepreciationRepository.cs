using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.PrePaymentDepreciations
{
    public interface IPrePaymentDepreciationRepository
    {
        public Task<bool> Add(PrePaymentDepreciation paymentDepreciation);
        public Task<bool> Update(PrePaymentDepreciation prePaymentDepreciation);
        public Task<PrePaymentDepreciation> Get(Guid id);
        public Task<PrePaymentDepreciation> GetByFinancialId(Guid id);
        public Task<List<PrePaymentDepreciation>> GetAllByInvoiceId(Guid invoiceId);
        public Task<List<Guid>> guids(string connectionString);
        public Task<List<PrePaymentDepreciation>> GetAllByFinancialId (Guid financialId);
        public Task<List<PrePaymentDepreciation>> GetAllByServiceExplenationId(Guid financialId);
        public Task<List<PrePaymentDepreciation>> GetAllForContract(Guid contractId);
    }
}
