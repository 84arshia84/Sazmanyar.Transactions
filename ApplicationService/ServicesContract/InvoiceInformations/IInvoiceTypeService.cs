using ApplicationService.DtoModels.InvoiceDtos.InvoiceTypeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.InvoiceInformations
{
    public interface IInvoiceTypeService
    {
        public Task<List<InvoiceTypeDto>> GetAll();
        public Task<List<InvoiceTypeDto>> GetAllInvoiceTypesWithAccessGroupEffect(string fullqualifyname,int mode);
        public Task<bool> Update(Guid id, Guid officeOnlineId);
    }
}
