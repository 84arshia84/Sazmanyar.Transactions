using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.Services.SettingServices;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class DefaultCoefficientsController : BaseController
    {
        private readonly IDefaultCoefficientsService _defaultCoefficientsService;
        public DefaultCoefficientsController(IDefaultCoefficientsService defaultCoefficientsService)
        {
            _defaultCoefficientsService = defaultCoefficientsService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] DefaultCoefficientsDto obj)
        {
            var result = await _defaultCoefficientsService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _defaultCoefficientsService.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] DefaultCoefficientsDto obj)
        {
            var result = await _defaultCoefficientsService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _defaultCoefficientsService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
