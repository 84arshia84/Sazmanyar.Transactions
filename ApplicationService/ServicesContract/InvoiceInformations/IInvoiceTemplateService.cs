using ApplicationService.DtoModels.InvoiceDtos.InvoiceDataForTemplateDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.InvoiceInformations
{
    public interface IInvoiceTemplateService
    {
        public Task<InvoiceGetDataForTemplateDto> GetTemplateData(Guid InvoiceId);
    }
}
