using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class HowToPayController : BaseController
    {
        private readonly IHowToPayService _howToPay;
        public HowToPayController(IHowToPayService howToPay)
        {
            _howToPay = howToPay;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] HowToPayDto obj)
        {
            var result = await _howToPay.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _howToPay.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] HowToPayDto obj)
        {
            var result = await _howToPay.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _howToPay.Delete(id);
            return Ok(result);
        }
    }
}
