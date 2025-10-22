using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.InvoiceAmounts
{
    public interface IInvoiceAmountRepository
    {
        public Task AddInvoiceAmount (InvoiceAmount invoiceAmount);
        public Task<List<InvoiceAmount>> GetAllInvoiceAmount();
        public Task<List<InvoiceAmount>> GetAllInvoiceAmount(Guid invoiceId);
        public Task<InvoiceAmount> GetInvoiceAmount (Guid invoiceId);
        public Task UpdateRequestedAmount(decimal amount, Guid invoiceId, Guid CurrencyId);
        public Task UpdateApprovedAmount (decimal amount, Guid invoiceId, Guid CurrencyId);
        public Task UpdateNettingAmount(decimal amount, Guid invoiceId, Guid CurrencyId);
        public Task DeleteApprovedAmount(Guid invoiceId);
        public Task DeleteAmounts(Guid invoiceId);
    }
}
