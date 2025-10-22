using AppCore.Entities.ContractAccessGroups;
using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceAccessGroupsDtos;
using ApplicationService.ServicesContract.InvoiceInformations;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.InvoiceInformationsControllers
{
    public class InvoiceAccessGroupController : BaseController
    {
        private readonly IInvoiceAccessGroupService _invoiceAccessGroupService;
        public InvoiceAccessGroupController(IInvoiceAccessGroupService invoiceAccessGroupService)
        {
            _invoiceAccessGroupService = invoiceAccessGroupService;
        }
        [HttpGet("GetAllInvoiceAccessGroups")]
        public async Task<List<AllInvoiceAccessGroupsDto>> GetAllInvoiceAccessGroups()
        {
            return await _invoiceAccessGroupService.GetAllInvoiceAccessGroups();
        }

        [HttpGet("GetInvoiceAccessGroupById")]
        public async Task<GetInvoiceAccessGroupDto> GetInvoiceAccessGroupById([FromQuery] Guid Id)
        {
            return await _invoiceAccessGroupService.GetInvoiceAccessGroupById(Id);
        }

        [HttpPost("AddInvoiceAccessGroup")]
        public async Task<IActionResult> AddInvoiceAccessGroup([FromBody] AddInvoiceAccessGroupDto invoiceAccessGroupDto)
        {
            await _invoiceAccessGroupService.AddInvoiceAccessGroup(invoiceAccessGroupDto);
            return Ok();
        }

        [HttpDelete("DeleteInvoiceAccessGroup")]
        public async Task<IActionResult> DeleteInvoiceAccessGroup([FromQuery] Guid Id)
        {
            await _invoiceAccessGroupService.DeleteInvoiceAccessGroup(Id);
            return Ok();
        }

        [HttpPut("UpdateInvoiceAccessGroup")]
        public async Task<IActionResult> UpdateInvoiceAccessGroup([FromQuery] Guid Id, [FromBody] AddInvoiceAccessGroupDto invoiceAccessGroup)
        {
            await _invoiceAccessGroupService.UpdateInvoiceAccessGroup(Id, invoiceAccessGroup);
            return Ok();
        }
        [HttpPost("GetAccessForDeleteAndEdit")]
        public async Task<IActionResult> GetAccessForDeleteAndEdit([FromQuery] Guid userId, [FromQuery] Guid entityId, [FromQuery] int part)
        {
            var result = await _invoiceAccessGroupService.UserAccessOnEditAndDelete(userId, entityId, part);
            return Ok(result);
        }
    }
}
