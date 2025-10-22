using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.InvoiceInformations;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.InvoiceInformationsControllers
{
    public class InvoiceAmountController : BaseController
    {
        private readonly IInvoiceAmountService _invoiceAmountService;
        public InvoiceAmountController(IInvoiceAmountService invoiceAmountService)
        {
            _invoiceAmountService = invoiceAmountService;
        }
        [HttpPut("UpdateNettingAmount")]
        public async Task<IActionResult> UpdateNettingAmount([FromBody] List<NettedAmountGetDto> amounts, [FromQuery] Guid invoiceId )
        {
            await _invoiceAmountService.UpdateNettingAmount(amounts, invoiceId);
            return Ok();
        }
    }
}
