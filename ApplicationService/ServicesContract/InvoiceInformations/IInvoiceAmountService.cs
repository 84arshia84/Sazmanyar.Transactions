using AppCore.Entities.InvoiceInformations.InvoiceAmounts;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.InvoiceInformations
{
    public interface IInvoiceAmountService
    {
        public Task AddInvoiceAmount(Guid invoiceId);
        public Task<List<InvoiceAmount>> GetInvoiceAmount(Guid invoiceId);
        public Task UpdateRequestedAmount(Guid invoiceId, bool rejected);
        public Task UpdateApprovedAmount(Guid invoiceId, bool rejected);
        public Task UpdateNettingAmount(List<NettedAmountGetDto> nettedValues, Guid invoiceId);
    }
}
