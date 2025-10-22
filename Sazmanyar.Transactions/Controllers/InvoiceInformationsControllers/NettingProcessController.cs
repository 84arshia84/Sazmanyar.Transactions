using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.InvoiceInformations;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.InvoiceInformationsControllers
{
    public class NettingProcessController : BaseController
    {
        private readonly INettingProcessItemService _nettingProcessItemService;
        public NettingProcessController(INettingProcessItemService nettingProcessItemService)
        {
            _nettingProcessItemService = nettingProcessItemService;
        }
        [HttpPost("AddNettingProcessItem")]
        public async Task<IActionResult> AddNettingProcessItem([FromBody] AddNettingProcessItemDto obj)
        {
            var user = new LoginUserDto();
            var result = await _nettingProcessItemService.AddNettingProcessItem(obj, user);
            return Ok(result);
        }
        [HttpPut("UpdateNettingProcessItem")]
        public async Task<IActionResult> UpdateNettingProcessItem([FromBody] UpdateNettingProcessItemDto obj)
        {
            var user = new LoginUserDto();
            var result = await _nettingProcessItemService.UpdateNettingProcessItem(obj, user);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("DeleteNettingProcessItem")]
        public async Task<IActionResult> DeleteNettingProcessItem([FromQuery] Guid nettingProcessItemId)
        {
            var user = new LoginUserDto();
            var result = await _nettingProcessItemService.DeleteNettingProcessItem(nettingProcessItemId, user);
            return Ok(result);
        }
        [HttpGet("GetAllNettingProcessItems")]
        public async Task<IActionResult> GetAllNettingProcessItems([FromQuery] Guid invoiceBaseInformationId)
        {
            var result = await _nettingProcessItemService.GetAllNettingProcessItems(invoiceBaseInformationId);
            return Ok(result);
        }
        [HttpGet("GetInvoiceRequestedPriceSummation")]
        public async Task<IActionResult> GetInvoiceRequestedPriceSummation([FromQuery] Guid invoiceBaseInformationId)
        {
            var result = await _nettingProcessItemService.GetInvoiceRequestedPriceSummation(invoiceBaseInformationId);
            return Ok(result);
        }
        [HttpGet("GetInvoiceApprovedPriceSummation")]
        public async Task<IActionResult> GetInvoiceApprovedPriceSummation([FromQuery] Guid invoiceBaseInformationId)
        {
            var result = await _nettingProcessItemService.GetInvoiceApprovedPriceSummation(invoiceBaseInformationId);
            return Ok(result);
        }
        [HttpGet("GetContractInvoicesRequestedPriceSummation")]
        public async Task<IActionResult> GetContractInvoicesRequestedPriceSummation( [FromQuery] Guid invoiceBaseInformationId)
        {
            var result = await _nettingProcessItemService.GetContractInvoicesRequestedPriceSummation( invoiceBaseInformationId);
            return Ok(result);
        }
        [HttpGet("GetContractInvoicesApprovedPriceSummation")]
        public async Task<IActionResult> GetContractInvoicesApprovedPriceSummation( [FromQuery] Guid invoiceBaseInformationId)
        {
            var result = await _nettingProcessItemService.GetContractInvoicesApprovedPriceSummation( invoiceBaseInformationId);
            return Ok(result);
        }
        [HttpGet("GetInvoiceNetPrice")]
        public async Task<IActionResult> GetInvoiceNetPrice([FromQuery] Guid invoiceBaseInformationId)
        {
            var result = await _nettingProcessItemService.GetInvoiceNetPrice(invoiceBaseInformationId);
            return Ok(result);
        }
        [HttpGet("GetNetPriceUnitlThisInvoice")]
        public async Task<IActionResult> GetNetPriceUnitlThisInvoice([FromQuery] Guid invoiceId)
        {
            var result = await _nettingProcessItemService.GetNetPriceUnitlThisInvoice(invoiceId);
            return Ok(result);
        }
    }
}
