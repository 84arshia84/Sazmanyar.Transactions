using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.DtoModels.InvoiceDtos.PaymentDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.InvoiceInformations;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.InvoiceInformationsControllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddPaymentDto obj)
        {
            var user = new LoginUserDto();
            var result = await _paymentService.Add(obj, user);
            return Ok(new { message = result.message, isSuccess = result.isSuccess});
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdatePaymentDto obj)
        {
            var user = new LoginUserDto();
            var result = await _paymentService.Update(obj, user);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid paymentId)
        {
            var user = new LoginUserDto();
            var result = await _paymentService.Delete(paymentId, user);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllNettingProcessItems([FromQuery] Guid invoiceBaseInformationId)
        {
            var result = await _paymentService.GetAll(invoiceBaseInformationId);
            return Ok(result);
        }
        [HttpGet("GetInvoicePaidPrice")]
        public async Task<IActionResult> GetInvoicePaidPrice([FromQuery] Guid invoiceBaseInformationId)
        {
            var result = await _paymentService.GetInvoicePaidPrice(invoiceBaseInformationId);
            return Ok(result);
        }
        [HttpGet("GetContractPaidPrice")]
        public async Task<IActionResult> GetContractPaidPrice([FromQuery] Guid contractId)
        {
            var result = await _paymentService.GetContractPaidPrice(contractId);
            return Ok(result);
        }
        [HttpGet("GetPaidPriceUntilThisInvoice")]
        public async Task<IActionResult> GetPaidPriceUntilThisInvoice([FromQuery] Guid invoiceBaseInformationId)
        {
            var result = await _paymentService.GetAllBeforThisInvoiceBaseInformationId(invoiceBaseInformationId);
            return Ok(result);
        }
    }
}
