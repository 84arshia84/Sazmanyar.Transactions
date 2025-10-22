using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class PaymentMethodController : BaseController
    {
        private readonly IPaymentMethodService _paymentMethodService;
        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] PaymentMethodDto obj)
        {
            var result = await _paymentMethodService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccss });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _paymentMethodService.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] PaymentMethodDto obj)
        {
            var result = await _paymentMethodService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccss });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _paymentMethodService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccss });
        }
    }
}
