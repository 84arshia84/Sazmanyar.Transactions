using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class CurrencyController : BaseController
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CurrencyDto obj)
        {
            var result = await _currencyService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccss });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _currencyService.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CurrencyDto obj)
        {
            var result = await _currencyService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccss });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _currencyService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccss });
        }
    }
}
