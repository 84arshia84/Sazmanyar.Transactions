using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials
{
    public interface IServiceExplanationFinancialRepository
    {
        Task<List<ServiceExplanationFinancial>> GetAllByInvoiceBaseInformationId(Guid invoiceBaseInformationId);
        Task<List<ServiceExplanationFinancial>> GetAllByServiceExplanationId(Guid serviceExplanationId);
        Task<List<ServiceExplanationFinancial>> GetAllBeforThisInvoiceBaseInformationId(Guid invoiceBaseInformationId);
        Task<List<ServiceExplanationFinancial>> GetAllByInvoiceType(Guid serviceExplenationId,Guid InvoiceTypeId, DateTime date);
        Task<List<ServiceExplanationFinancial>> GetAllByInvoiceType(List<Guid> serviceExplenationId, Guid InvoiceTypeId, DateTime date);
        Task<ServiceExplanationFinancial> Get(Guid serviceExplanationFinancialId);
        Task<(string message, bool isSuccess)> Add(ServiceExplanationFinancial serviceExplanationFinancial);
        Task<(string message, bool isSuccess)> Update(ServiceExplanationFinancial serviceExplanationFinancial);
        Task<(string message, bool isSuccess)> Delete(Guid serviceExplanationFinancialId);
        Task<(string message, bool isSuccess)> DeleteByInvoiceId(Guid InvoiceId);
    }
}
