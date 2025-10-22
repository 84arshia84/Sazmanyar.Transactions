using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.EstimatedMeterFinancials
{
    public interface IEstimatedMeterFinancialRepository
    {
        public Task<List<EstimatedMeterFinancial>> GetAllByInvoiceBaseInformationId(Guid invoiceBaseInformationId);
        public Task<EstimatedMeterFinancial> Get(Guid estimatedMeterFinancialId);
        public Task<(string message, bool isSuccess)> Add(EstimatedMeterFinancial estimatedMeterFinancial);
        public Task<(string message, bool isSuccess)> Delete(Guid estimatedMeterFinancialId);
    }
}
