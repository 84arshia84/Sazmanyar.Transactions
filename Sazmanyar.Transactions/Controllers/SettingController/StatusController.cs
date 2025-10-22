using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class StatusController : BaseController
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] StatusDto obj)
        {
            var result = await _statusService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _statusService.GetAll();
            return Ok(result);
        }
        [HttpPost("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _statusService.Get(id);
            return Ok(result);
        }
        [HttpPost("GetByContractId")]
        public async Task<IActionResult> GetByContractId([FromQuery] Guid id)
        {
            var result = await _statusService.GetByContractId(id);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] StatusDto obj)
        {
            var result = await _statusService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _statusService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
