using AppCore.Entities.InvoiceInformations.OnAccountDepreciations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.InvoiceInformations
{
    public interface IAccountService
    {
        public Task<bool> Add(OnAccountDepreciation onAccountDepreciation);
        public Task<bool> Update(OnAccountDepreciation onAccountDepreciation);
        public Task migrateData();
        public Task<OnAccountDepreciation> Get(Guid id);
        public Task<List<OnAccountDepreciation>> GetAllByInvoiceId(Guid invoiceId);
        public Task<List<OnAccountDepreciation>> GetAllByFinancialId(Guid financialId);
        public Task CalculatingSugestedOnAccount(Guid ServiceExplenationFinancialId);
        public Task<decimal> CalculatingNetProccessForOnAccount(Guid invoiceBaseInformationId, Guid currencyId);
        public Task<bool> TheImpactOfTheUserAmountOnTheRemainingDepreciation(Guid invoiceBaseInformationId, decimal value);
    }
}
