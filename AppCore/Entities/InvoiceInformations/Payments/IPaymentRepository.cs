using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceInformations.NettingProcesses;

namespace AppCore.Entities.InvoiceInformations.Payments
{
    public interface IPaymentRepository
    {
        Task<(string message, bool isSuccess)> Add(Payment payment);
        Task<Payment> Get(Guid paymentId);
        Task<(string message, bool isSuccess)> Delete(Guid paymentId);
        Task<(string message, bool isSuccess)> DeleteByInvoiceId(Guid invoiceId);
        Task<List<Payment>> GetAll(Guid invoiceBaseInformationId);
        Task<List<Payment>> GetAllBeforThisInvoiceBaseInformationId(Guid invoiceBaseInformationId);
    }
}
