using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseCustomizationDtos;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.ServicesContract.InvoiceInformations;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.InvoiceInformationsControllers
{
    public class InvoiceBaseCustomizationController : BaseController
    {
        private readonly IInoviceBaseCustomizationService _service;
            public InvoiceBaseCustomizationController(IInoviceBaseCustomizationService service)
            {
                _service = service;
            }

            [HttpPut("Update")]
            public async Task<IActionResult> Update( InvoiceBaseCustomizationDto dto)
            {
                await _service.Update(dto);
                return Ok();
            }
            [HttpGet("Get")]
            public async Task<IActionResult> Get([FromQuery] Guid id)
            {
                await _service.Get(id);
                return Ok();
            }
        
    }
}
