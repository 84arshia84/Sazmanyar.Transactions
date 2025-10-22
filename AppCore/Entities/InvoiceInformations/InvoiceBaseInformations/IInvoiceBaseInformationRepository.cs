using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.InvoiceBaseInformations
{
    public interface IInvoiceBaseInformationRepository
    {
        Task<(string message, bool isSuccess, Guid invoiceBaseInformationId)> Add(InvoiceBaseInformation invoiceBaseInformation);
        Task<(string message, bool isSuccess)> Update(InvoiceBaseInformation invoiceBaseInformation);
        Task<(string message, bool isSuccess)> Delete(Guid invoiceBaseInformationid);
        Task<InvoiceBaseInformation> Get(Guid invoiceBaseInformationid);
        Task<List<InvoiceBaseInformation>> GetAll();
        Task<List<InvoiceBaseInformation>> GetAllMine(Guid userId);
        Task<List<Guid>> GetAllMineIds(Guid userId);
        Task<List<InvoiceBaseInformation>> GetAllWaitingForAction(List<Guid>? waitingForActionIds);
        Task<List<Guid>> GetAllIdsByUserAccessGroups(Guid userId, string connectionString);
        Task<List<InvoiceBaseInformation>> GetInvoiceBaseInformationByIds(List<Guid>? Ids);
        Task<List<InvoiceBaseInformation>> GetAllByContractId(Guid contractId);
        Task<List<InvoiceBaseInformation>> GetAllByContractIdBefor(Guid contractId, DateTime beforThisInvoice);
        Task<List<InvoiceBaseInformation>> SearchAllAsync(string? term);
        Task<List<InvoiceBaseInformation>> SearchMineAsync(Guid userId, string? term);
        Task<List<InvoiceBaseInformation>> SearchWaitForActionAsync(List<Guid> waitingForActionIds, string? term);
        Task<List<InvoiceBaseInformation>> SearchByContractAsync(Guid contractIds, string? term);
        // بررسی تکراری بودن شماره صورت وضعیت
        Task<InvoiceBaseInformation?> GetByInvoiceNumber(string invoiceCode);


    }
}
