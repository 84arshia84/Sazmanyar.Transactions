using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class FinePaymentMethodController : BaseController
    {
        private readonly IFinePaymentMethodService _finePaymentMethod;
        public FinePaymentMethodController(IFinePaymentMethodService finePaymentMethod)
        {
            _finePaymentMethod = finePaymentMethod;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] FinePaymentMethodDto obj)
        {
            var result = await _finePaymentMethod.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _finePaymentMethod.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] FinePaymentMethodDto obj)
        {
            var result = await _finePaymentMethod.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _finePaymentMethod.Delete(id);
            return Ok(result);
        }
    }
}
