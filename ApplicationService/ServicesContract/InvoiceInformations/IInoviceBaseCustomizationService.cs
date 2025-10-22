using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseCustomizationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.InvoiceInformations
{
    public interface IInoviceBaseCustomizationService
    {
        public Task<InvoiceBaseCustomizationDto> Get(Guid id);
        public Task Update(InvoiceBaseCustomizationDto dto);
    }
}
