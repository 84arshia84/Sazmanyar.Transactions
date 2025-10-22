using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.NettingProcesses
{
    public interface INettingProcessItemRepository
    {
        public Task<(string message, bool isSuccess)> Add(NettingProcessItem nettingProcessItem);
        public Task<NettingProcessItem> Get(Guid nettingProcessItemId);
        public Task<List<NettingProcessItem>> GetAll(Guid invoiceBaseInformationId);
        public Task<(string message, bool isSuccess)> Delete(Guid nettingProcessItemId);
        Task<(string message, bool isSuccess)> DeleteByInvoiceId(Guid invoiceId);
    }
}
