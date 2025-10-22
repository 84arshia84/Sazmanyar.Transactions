using AppCore.Entities.InvoiceInformations.PrePaymentDepreciations;
using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.InvoiceInformations
{
    public interface IPrePaymentDepreciationService
    {
        public Task<bool> Add(PrePaymentDepreciation paymentDepreciation);
        public Task migrateData();
        public Task<bool> Update(PrePaymentDepreciation prePaymentDepreciation);
        public Task<PrePaymentDepreciation> Get(Guid id);
        public Task<List<PrePaymentDepreciation>> GetAllByInvoiceId(Guid invoiceId);
        public Task<List<PrePaymentDepreciation>> GetAllByFinancialId(Guid financialId);
        public Task CalculatingSugestedPrepayment(Guid ServiceExplenationFinancialId);
        public Task<decimal> CalculatingNetProccessForPrepayment(Guid invoiceBaseInformationId, Guid currencyId);
        public Task<bool> TheImpactOfTheUserAmountOnTheRemainingDepreciation(Guid invoiceBaseInformationId,decimal value);
    }
}
