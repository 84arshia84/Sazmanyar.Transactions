using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceAccessGroupsDtos;
using ApplicationService.DtoModels.UserDtos;

namespace ApplicationService.ServicesContract.InvoiceInformations
{
    public interface IInvoiceAccessGroupService
    {
        public Task<(string message, bool isSuccess)> AddInvoiceAccessGroup(AddInvoiceAccessGroupDto invoiceAccessGroupDto);
        public Task<(string message, bool isSuccess)> DeleteInvoiceAccessGroup(Guid Id);
        public Task<(string message, bool isSuccess)> UpdateInvoiceAccessGroup(Guid id, AddInvoiceAccessGroupDto invoiceAccessGroupDto);
        public Task<GetInvoiceAccessGroupDto> GetInvoiceAccessGroupById(Guid Id);
        public Task<List<AllInvoiceAccessGroupsDto>> GetAllInvoiceAccessGroups();
        public Task<UserAccessOnEditAndDelete> UserAccessOnEditAndDelete(Guid userId, Guid partEntityId, int part);
    }
}
