using ApplicationService.DtoModels.DesignCodeDtos.FactorDesignCodeDtos;
using ApplicationService.DtoModels.DesignCodeDtos.InvoiceDesignCodeDtos;
using ApplicationService.Services.DesignCodeServices;
using ApplicationService.ServicesContract.DesignCode;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.DesignCodeControllers
{
    public class InvoiceDesignCodeController : BaseController
    {
        private readonly IInvoiceDesignCodeService _invoiceDesignCodeService;
        public InvoiceDesignCodeController(IInvoiceDesignCodeService invoiceDesignCodeService)
        {
            _invoiceDesignCodeService = invoiceDesignCodeService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] InvoiceDesignCodeAddDto obj)
        {
            var result = await _invoiceDesignCodeService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _invoiceDesignCodeService.GetAll();
            return Ok(result);
        }
        [HttpPost("GetAllParameter")]
        public async Task<IActionResult> GetAllParameter()
        {
            var result = await _invoiceDesignCodeService.GetAllParameter();
            return Ok(result);
        }
        [HttpPost("GetAllParameterById")]
        public async Task<IActionResult> GetAllForContract([FromQuery] Guid id)
        {
            var result = await _invoiceDesignCodeService.GetAllParameterById(id);
            return Ok(result);
        }
        [HttpPost("GenerateDesignCode")]
        public async Task<IActionResult> GenerateDesignCode([FromBody] InvoiceDesignCodeSearchParameterDto obj)
        {
            var result = await _invoiceDesignCodeService.GenerateDesignCode(obj);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] InvoiceDesignCodeUpdateDto obj)
        {
            var result = await _invoiceDesignCodeService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpPut("UpdateCounter")]
        public async Task<IActionResult> UpdateCounter([FromQuery] Guid id)
        {
            await _invoiceDesignCodeService.UpdateCounter(id);
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _invoiceDesignCodeService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
