using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.InvoiceInformations;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.InvoiceInformationsControllers
{
    public class InvoiceTemplateDataController : BaseController
    {
        private readonly IInvoiceTemplateService _invoiceTemplateService;
        public InvoiceTemplateDataController(IInvoiceTemplateService invoiceTemplateService)
        {
            _invoiceTemplateService = invoiceTemplateService;
        }
        [HttpPost("GetTemplateData")]
        public async Task<IActionResult> GetTemplateData([FromQuery] Guid id)
        {
            var result = await _invoiceTemplateService.GetTemplateData(id);
            return Ok(result);
        }
    }
}
